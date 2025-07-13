using DeliveryInternation2._0.Applications.Services;
using DeliveryInternation2._0.Data;
using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryInternation2._0.Applications.BusinessLogics
{
    public class CartService:ICartService
    {
        private readonly DataContext _dataContext;

        public CartService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> AddDishToCart(string email, Guid idDish)
        {
            var user = await _dataContext.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.DishInCarts)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("You are not authorized. Please authorize yourself.");
            }

            var dish = await _dataContext.Dishes.FirstOrDefaultAsync(d => d.Id == idDish);

            if (dish == null)
            {
                throw new KeyNotFoundException("Dish can not be found.");
            }

            var addDish = new DishInCart
            {
                Count = 1,
                Dish = dish,
                Cart = user.Cart
            };

            user.Cart.Amount += dish.Price;
            user.Cart.DishInCarts.Add(addDish);

            _dataContext.DishesInCart.Add(addDish);

            await _dataContext.SaveChangesAsync();

            return "Dish added to cart.";
            
        }

        public async Task<string> IncreaseOrDecreaseDish(string email, Guid idDish, bool isIncrease)
        {
            var user = await _dataContext.Users
                .Include(u => u.Cart)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("You are not authorized. Please authorize yourself.");
            }

            var dish = await _dataContext.Dishes.FirstOrDefaultAsync(d => d.Id == idDish);

            if (dish == null)
            {
                throw new KeyNotFoundException("Dish can not be found.");
            }

            var dishInCart = await _dataContext.DishesInCart.FirstOrDefaultAsync(d => d.Dish.Id == idDish);

            if(dishInCart == null)
            {
                throw new KeyNotFoundException("Dish is not in the cart.");
            }

            string result;

            if (isIncrease)
            {
                dishInCart.Count += 1;
                dishInCart.Cart.Amount += dish.Price;
                result = "Dish increased!";
            }
            else
            {
                if(dishInCart.Count == 1)
                {
                    _dataContext.DishesInCart.Remove(dishInCart);
                    result = "Dish removed!";
                }
                else
                {
                    dishInCart.Count -= 1;
                    dishInCart.Cart.Amount -= dish.Price;
                    result = "Dish decreased!";
                }
            }

            await _dataContext.SaveChangesAsync();

            return result;

        }

        public async Task<string> RemoveDish(string email, Guid idDish)
        {
            var dish = await _dataContext.DishesInCart.FirstOrDefaultAsync(d => d.Cart.User.Email == email && d.Dish.Id == idDish);

            if(dish == null)
            {
                throw new KeyNotFoundException("Dish can not be found.");
            }

            _dataContext.Remove(dish);
            await _dataContext.SaveChangesAsync();

            return "Dish removed!";
        }
        
        public async Task<GetCartDto> GetCart (string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("You are not authorized. Please authorize yourself.");
            }

            var cart = await _dataContext.Carts
                .Include(c => c.DishInCarts)
                .ThenInclude(d => d.Dish)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                throw new KeyNotFoundException("Cart can not be found.");
            }

            var userCart = new GetCartDto
            {
                Id = cart.Id,
                Amount = cart.Amount,
                DishInCarts = cart.DishInCarts.Select( d => new DishInCartDto
                {
                    Id = d.Dish.Id,
                    Name = d.Dish.Name,
                    Price = d.Dish.Price,
                    Image = d.Dish.Image,
                    Count = d.Count,
                    TotalPrice = d.Dish.Price * d.Count
                }).ToList(),
            };

            return userCart;
        }
    }
}

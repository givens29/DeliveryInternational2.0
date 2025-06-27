using DeliveryInternation2._0.Applications.Services;
using DeliveryInternation2._0.Data;
using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;
using DeliveryInternation2._0.Models.Enums;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DeliveryInternation2._0.Applications.BusinessLogics
{
    public class DishService : IDishService
    {
        private readonly DataContext _dataContext;

        public DishService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Dish> AddDish(AddDishDto newDish)
        {
            var dish = new Dish
            {
                Name = newDish.Name,
                Description = newDish.Description,
                Price = newDish.Price,
                Image = newDish.Image,
                IsVegetarian = newDish.IsVegetarian,
                Category = newDish.Category
            };

            await _dataContext.Dishes.AddAsync(dish);
            await _dataContext.SaveChangesAsync();

            return dish;
        }

        public async Task<ListDishesDto> GetDishesByFilters(Category? category, bool isVegetarian, int pageNum, Sort? sort)
        {
            IQueryable<Dish> dishQuery = _dataContext.Dishes.AsQueryable();

            if (category.HasValue)
            {
                dishQuery = dishQuery.Where(d => d.Category == category.Value);
            }
            if (isVegetarian)
            {
                dishQuery = dishQuery.Where(d => d.IsVegetarian);
            }
            else
            {
                dishQuery = dishQuery.Where(d => !d.IsVegetarian);
            }

            switch(sort)
            {
                case Sort.NameAsc:
                    dishQuery = dishQuery.OrderBy(d => d.Name);
                    break;
                case Sort.NameDesc:
                    dishQuery = dishQuery.OrderByDescending(d => d.Name);
                    break;
                case Sort.PriceAsc:
                    dishQuery = dishQuery.OrderBy(d => d.Price);
                    break;
                case Sort.PriceDesc:
                    dishQuery = dishQuery.OrderByDescending(d => d.Price);
                    break;
                case Sort.RatingAsc:
                    dishQuery = dishQuery.OrderBy(d => d.Ratings);
                    break;
                case Sort.RatingDesc:
                    dishQuery = dishQuery.OrderByDescending(d => d.Ratings);
                    break;
                default:
                    dishQuery = dishQuery.OrderBy(d => d.Name);
                    break;
            }

            int pageSize = 5;

            int pageToQuery = Math.Max(pageNum, 1);

            int skip = (pageToQuery - 1) * pageSize;

            int totalCount = await dishQuery.CountAsync();

            dishQuery = dishQuery.Skip(skip).Take(pageSize);

            var pagination = new PaginationDto
            {
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                CurrentPage = pageToQuery
            };

            var result = new ListDishesDto
            {
                dishes = await dishQuery.ToListAsync(),
                pagination = pagination
            };

            return result;
        }
        
        public async Task<Dish> GetConcreteDish(Guid idDish)
        {
            var dish = await _dataContext.Dishes.FirstOrDefaultAsync(d => d.Id == idDish);

            if(dish == null)
            {
                throw new KeyNotFoundException("Dish can not be found.");
            }

            return dish;
        }

        public async Task<Dish> AddDishRating(string email, Guid idDish, int rating)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            if(user == null)
            {
                throw new UnauthorizedAccessException("You are not authorized. Please authorize yourself.");
            }

            var dish = await _dataContext.Dishes.FirstOrDefaultAsync(d => d.Id == idDish);

            if (dish == null)
            {
                throw new KeyNotFoundException("Dish can not be found.");
            }

            var dishRated = await _dataContext.Ratings.FirstOrDefaultAsync(r => r.User.Email == email && r.Dish.Id == idDish);

            if (dishRated != null)
            {
                throw new ArgumentException("You rated this dish.");
            }


            var userRating = new Rating
            {
                User = user,
                Dish = dish,
                Value = rating
            };

            user.Ratings ??= new List<Rating>();
            dish.Ratings ??= new List<Rating>();


            dish.Ratings.Add(userRating);
            user.Ratings.Add(userRating);

            await _dataContext.Ratings.AddAsync(userRating);
            await _dataContext.SaveChangesAsync();

            return dish;

        }
    
    }
}

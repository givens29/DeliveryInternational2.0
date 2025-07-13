using DeliveryInternation2._0.Applications.Services;
using DeliveryInternation2._0.Data;
using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;
using DeliveryInternation2._0.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DeliveryInternation2._0.Applications.BusinessLogics
{
    public class OrderService:IOrderService
    {
        private readonly DataContext _dataContext;
        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Order> CreateOrder(OrderDto orderDto)
        {
            var cart = await _dataContext.Carts
                .Include(c => c.DishInCarts)
                    .ThenInclude(d => d.Dish)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.User.Email == orderDto.email);

            if (cart == null)
                throw new InvalidOperationException("Cart not found.");

            if (!cart.DishInCarts.Any())
                throw new InvalidOperationException("Cart is empty.");

            var order = new Order
            {
                PhoneNumber = orderDto.phoneNumber,
                Email = orderDto.email,
                DeliveryTime = orderDto.dateTimeDelivery,
                Address = orderDto.address,
                Price = cart.Amount,
                Status = Status.InProcess,
                OrderTime = DateTime.Now,
                Cart = cart,
                DishInOrders = new List<DishInOrder>()
            };

            foreach (var item in cart.DishInCarts)
            {
                var dishInOrder = new DishInOrder
                {
                    Dish = item.Dish,
                    Count = item.Count,
                    Order = order
                };

                order.DishInOrders.Add(dishInOrder);
            }

            await _dataContext.Orders.AddAsync(order);

            cart.DishInCarts.Clear();
            cart.Amount = 0;

            await _dataContext.SaveChangesAsync();

            return order;
        }


        public async Task<GetOrderDto> GetConcreateOrder(Guid idOrder)
        {
            var order = await _dataContext.Orders
                .Include(o => o.DishInOrders)
                .ThenInclude( d => d.Dish)
                .FirstOrDefaultAsync(o => o.id == idOrder);

            if(order == null)
            {
                throw new KeyNotFoundException("Order can not be found.");
            }

            var orderDto = new GetOrderDto
            {
                id = order.id,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                Price = order.Price,
                Address = order.Address,
                Status = order.Status,
                DishInCart = order.DishInOrders.Select(d => new DishInCartDto
                {
                    Id = d.Id,
                    Name = d.Dish.Name,
                    Image = d.Dish.Image,
                    Count = d.Count,
                    Price = d.Dish.Price,
                    TotalPrice = d.Dish.Price * d.Count
                }).ToList()
            };

            return orderDto;
        }

        public async Task<List<Order>> GetListOrders(string email)
        {
            var orders = await _dataContext.Orders.Where(o => o.Cart.User.Email == email).ToListAsync();

            return orders;
        }

        public async Task<string> ConfirmDelivery(Guid idOrder)
        {
            var order = await _dataContext.Orders
                .FirstOrDefaultAsync(o => o.id == idOrder);
            
            if(order == null)
            {
                throw new KeyNotFoundException("Order can not be found.");
            }

            order.OrderTime = DateTime.Now;
            order.Status = Status.Delivered;

            await _dataContext.SaveChangesAsync();
            return "Order Delivered.";
        }
    }
}

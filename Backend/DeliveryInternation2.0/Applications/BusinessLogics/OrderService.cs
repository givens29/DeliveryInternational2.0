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

        public async Task<Order> CreateOrder(string email, string address)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("You are not authorized. Please authorize yourself.");
            }

            var cart = await _dataContext.Carts
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.User.Email == email);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart can not be found.");
            }

            var order = new Order
            {
                DeliveryTime = DateTime.UtcNow,
                Address = address,
                Price = cart.Amount,
                Status = Status.InProcess,
                CardId = cart.Id,
                Cart = cart
            };

            if(cart.Orders == null)
            {
                cart.Orders = new List<Order> {order};
            }
            else
            {
                cart.Orders.Add(order);
            }

            await _dataContext.Orders.AddAsync(order);
            await _dataContext.SaveChangesAsync();

            return order;
        }
    
        public async Task<Order> GetConcreateOrder(Guid idOrder)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.id == idOrder);

            if(order == null)
            {
                throw new KeyNotFoundException("Order can not be found.");
            }

            return order;
        }

        public async Task<List<Order>> GetListOrders(string email)
        {
            var orders = await _dataContext.Orders.Where(o => o.Cart.User.Email == email).ToListAsync();

            return orders;
        }

        public async Task<string> ConfirmDelivery(Guid idOrder)
        {
            var order = await _dataContext.Orders
                .Include(o => o.Cart)
                .ThenInclude(c => c.DishInCarts)
                .FirstOrDefaultAsync(o => o.id == idOrder);
            
            if(order == null)
            {
                throw new KeyNotFoundException("Order can not be found.");
            }

            order.Status = Status.Delivered;
            order.Cart.Amount = 0;
            order.Cart.DishInCarts.Clear();
            await _dataContext.SaveChangesAsync();
            return "Order Delivered.";
        }
    }
}

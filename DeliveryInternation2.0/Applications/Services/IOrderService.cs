using DeliveryInternation2._0.Models;

namespace DeliveryInternation2._0.Applications.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string email, string address);
        Task<Order> GetConcreateOrder(Guid idOrder);
        Task<List<Order>> GetListOrders(string email);
        Task<string> ConfirmDelivery(Guid idOrder);
    }
}

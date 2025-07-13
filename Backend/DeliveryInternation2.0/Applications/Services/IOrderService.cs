using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;

namespace DeliveryInternation2._0.Applications.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(OrderDto orderDto);
        Task<GetOrderDto> GetConcreateOrder(Guid idOrder);
        Task<List<Order>> GetListOrders(string email);
        Task<string> ConfirmDelivery(Guid idOrder);
    }
}

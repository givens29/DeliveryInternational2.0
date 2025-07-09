using DeliveryInternation2._0.Dtos;

namespace DeliveryInternation2._0.Applications.Services
{
    public interface ICartService
    {
        Task<string> AddDishToCart(string email, Guid idDish);
        Task<string> IncreaseOrDecreaseDish(string email, Guid idDish, bool isIncrease);
        Task<GetCartDto> GetCart(string email);
    }
}

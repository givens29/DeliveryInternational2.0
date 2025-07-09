using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;
using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Applications.Services
{
    public interface IDishService
    {
        Task<Dish> AddDish(AddDishDto newDish);
        Task<ListDishesDto> GetDishesByFilters(Category? category, bool? isVegetarian, int pageNum, Sort? sort);
        Task<Dish> GetConcreteDish(Guid idDish);
        Task<Dish> AddDishRating(string email, Guid idDish, int rating);
    }
}

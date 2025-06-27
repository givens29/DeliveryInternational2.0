using DeliveryInternation2._0.Models;

namespace DeliveryInternation2._0.Dtos
{
    public class ListDishesDto
    {
        public List<Dish> dishes {  get; set; }
        public PaginationDto pagination { get; set; }
    }
}

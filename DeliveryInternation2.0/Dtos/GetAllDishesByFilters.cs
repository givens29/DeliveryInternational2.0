using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Dtos
{
    public class GetAllDishesByFilters
    {
        public Category? Category { get; set; }
        public bool isVegetarian { get; set; } = false;
        public int pageNumber { get; set; } = 1;
        public Sort? sort { get; set; }
    }
}

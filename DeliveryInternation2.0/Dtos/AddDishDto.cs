using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Dtos
{
    public class AddDishDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public bool IsVegetarian { get; set; }
        public Category Category { get; set; }
    }
}

using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Models
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public bool IsVegetarian { get; set; }
        public List<Rating>? Ratings { get; set; }
        public Category Category { get; set; }
    }
}

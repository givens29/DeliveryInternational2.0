using DeliveryInternation2._0.Models.Enums;
using DeliveryInternation2._0.Models;

namespace DeliveryInternation2._0.Dtos
{
    public class DishInCartDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

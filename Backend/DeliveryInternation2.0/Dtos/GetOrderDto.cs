using DeliveryInternation2._0.Models.Enums;
using DeliveryInternation2._0.Models;
using System.Text.Json.Serialization;

namespace DeliveryInternation2._0.Dtos
{
    public class GetOrderDto
    {
        public Guid id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = string.Empty;
        public Status Status { get; set; }
        public List<DishInCartDto> DishInCart { get; set; } = new();
    }
}

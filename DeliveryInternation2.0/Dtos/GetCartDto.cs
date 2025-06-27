using DeliveryInternation2._0.Models;

namespace DeliveryInternation2._0.Dtos
{
    public class GetCartDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public List<DishInCartDto> DishInCarts { get; set; }
        
    }
}

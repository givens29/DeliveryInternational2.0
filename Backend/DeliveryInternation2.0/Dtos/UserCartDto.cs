namespace DeliveryInternation2._0.Dtos
{
    public class UserCartDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public List<DishInCartDto> DishInCarts { get; set; }
        public int DishNumber { get; set; }
    }
}

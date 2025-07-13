namespace DeliveryInternation2._0.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public List<DishInCart> DishInCarts { get; set; } = new List<DishInCart> ();
        public Guid UserId { get; set; }
        public User User { get; set; }

    }

}

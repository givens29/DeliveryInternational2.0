namespace DeliveryInternation2._0.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public List<DishInCart> DishInCarts { get; set; } = new List<DishInCart> ();
        public Order? Order { get; set; }


    }

}

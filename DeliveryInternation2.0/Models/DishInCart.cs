namespace DeliveryInternation2._0.Models
{
    public class DishInCart
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Dish Dish { get; set; } = new Dish();
    }
}

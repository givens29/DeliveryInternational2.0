namespace DeliveryInternation2._0.Models
{
    public abstract class DishItemBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Count { get; set; }
        public Dish Dish { get; set; }
    }
}

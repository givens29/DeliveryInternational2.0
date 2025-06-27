namespace DeliveryInternation2._0.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public User User { get; set; }
        public Dish Dish { get; set; }
    }
}

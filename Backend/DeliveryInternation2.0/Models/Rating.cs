namespace DeliveryInternation2._0.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public Guid UserId { get; set; }
        public Guid DishId { get; set; }
    }
}

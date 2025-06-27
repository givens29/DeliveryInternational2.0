namespace DeliveryInternation2._0.Models
{
    public class StorageUserToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}

using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public List<Rating>? Ratings { get; set; }
        public StorageUserToken? Token { get; set; }

    }
}

using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Dtos
{
    public class UserEditProfileDto
    {
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

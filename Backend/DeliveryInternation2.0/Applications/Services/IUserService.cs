using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;

namespace DeliveryInternation2._0.Applications.Services
{
    public interface IUserService
    {
        Task<string> Registration(UserRegistrationDto newUser);
        Task<string> Login(string email, string password);
        Task<string> Logout(string email);
        Task<UserProfileDto> GetProfile(string email);
        Task<string> EditProfile(string email, UserEditProfileDto updateProfile);
    }

}

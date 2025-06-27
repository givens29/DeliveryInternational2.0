using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeliveryInternation2._0.Applications.Services;
using DeliveryInternation2._0.Configurations;
using DeliveryInternation2._0.Data;
using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryInternation2._0.Applications.BusinessLogics
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly JwtBeareTokenSettings _jwtBeareTokenSettings;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public UserService(DataContext dataContext, IOptions<JwtBeareTokenSettings> jwtTokenOptions)
        {
            _dataContext = dataContext;
            _jwtBeareTokenSettings = jwtTokenOptions.Value;
        }

        public async Task<string> Registration(UserRegistrationDto newUser)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);

            if (user != null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            var registerUser = new User
            {
                FullName = newUser.FullName,
                BirthDate = newUser.BirthDate,
                Gender = newUser.Gender,
                Address = newUser.Address,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber
            };

            registerUser.Password = _passwordHasher.HashPassword(registerUser, newUser.Password); ;

            await _dataContext.Users.AddAsync(registerUser);

            string token = GenerateToken(registerUser);

            var userToken = new StorageUserToken
            {
                email = newUser.Email,
                Token = token,
                UserId = registerUser.Id,
                User = registerUser
            };

            await _dataContext.StorageUsersTokens.AddAsync(userToken);

            registerUser.Token = userToken;

            await _dataContext.SaveChangesAsync();

            return token;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _dataContext.Users
                .Include(u => u.Token)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new KeyNotFoundException("Invalid email or password.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if(result != PasswordVerificationResult.Success)
            {
                throw new KeyNotFoundException("Invalid email or password.");
            }

            var token = GenerateToken(user);

            var userToken = new StorageUserToken
            {
                email = user.Email,
                Token = token,
                User = user,
                UserId = user.Id
            };

            await _dataContext.StorageUsersTokens.AddAsync(userToken);
            await _dataContext.SaveChangesAsync();
            return token;
        }

        public async Task<bool> Logout(string email)
        {
            var userToken = await _dataContext.StorageUsersTokens.FirstOrDefaultAsync(t => t.email == email);

            if (userToken == null)
            {
                throw new KeyNotFoundException("User not login or register");
            }

            _dataContext.StorageUsersTokens.Remove(userToken);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserProfileDto> GetProfile(string email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("You are not authenticated. Please authenticated yourself");
            }

            var userProfile = new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return userProfile;
        }

        public async Task<User> EditProfile(string email, UserEditProfileDto updateProfile)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email );

            if (user == null)
            {
                throw new UnauthorizedAccessException("You are not authenticated. Please authenticated yourself");
            }

            if (!string.IsNullOrEmpty(updateProfile.FullName))
            {
                user.FullName = updateProfile.FullName;
            }
            if (updateProfile.BirthDate.HasValue)
            {
                user.BirthDate = updateProfile.BirthDate.Value;
            }
            if (updateProfile.Gender.HasValue)
            {
                user.Gender = updateProfile.Gender.Value;
            }
            if (!string.IsNullOrEmpty(updateProfile.Address))
            {
                user.Address = updateProfile.Address;
            }
            if (!string.IsNullOrEmpty(updateProfile.PhoneNumber))
            {
                user.PhoneNumber = updateProfile.PhoneNumber;
            }

            await _dataContext.SaveChangesAsync();

            return user;

        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBeareTokenSettings.SecretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Authentication, user.Id.ToString())

                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtBeareTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBeareTokenSettings.Audience,
                Issuer = _jwtBeareTokenSettings.Issuer,
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

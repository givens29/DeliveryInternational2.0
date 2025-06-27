using System.Security.Claims;
using DeliveryInternation2._0.Applications.Services;
using DeliveryInternation2._0.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryInternation2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration(UserRegistrationDto user)
        {
            try
            {
                var registration = await _userService.Registration(user);
                return Ok(registration);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var login = await _userService.Login(email, password);
                return Ok(login);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> Logout()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var logout = await _userService.Logout(emailClaim.Value);
                    return Ok(logout);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return Unauthorized("You are not authenticated. Please authenticated yourself");
            }
        }

        [HttpGet("getProfile")]
        [Authorize(Policy ="AnyAuthenticatedUser")]
        public async Task<IActionResult> GetProfile()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if(emailClaim != null)
            {
                try
                {
                    var profile = await _userService.GetProfile(emailClaim.Value);
                    return Ok(profile);
                }
                catch(UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return Unauthorized("You are not authenticated. Please authenticated yourself");
            }
        }

        [HttpPut("updateProfile")]
        [Authorize(Policy ="AnyAuthenticatedUser")]
        public async Task<IActionResult> UpdateProfile(UserEditProfileDto updateProfile)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if(emailClaim != null)
            {
                try
                {
                    var profile = await _userService.EditProfile(emailClaim.Value, updateProfile);
                    return Ok(profile);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return Unauthorized("You are not authenticated. Please authenticated yourself");
            }
        }
    }
}

using DeliveryInternation2._0.Applications.BusinessLogics;
using System.Security.Claims;
using DeliveryInternation2._0.Applications.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryInternation2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("addDishToCart")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> AddDishToCart(Guid idDish)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var result = await _cartService.AddDishToCart(emailClaim.Value, idDish);
                    return Ok(result);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
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

        [HttpPut("increaseOrDecreaseDish")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> IncreaseOrDecreaseDish(Guid idCart, Guid idDish, bool isIncrease)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var result = await _cartService.IncreaseOrDecreaseDish(emailClaim.Value,idDish,isIncrease);
                    return Ok(result);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
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

        [HttpGet("getCart")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> GetCart(Guid idCart)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var cart = await _cartService.GetCart(emailClaim.Value, idCart);
                    return Ok(cart);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
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
    }
}

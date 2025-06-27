using System.Security.Claims;
using DeliveryInternation2._0.Applications.Services;
using DeliveryInternation2._0.Dtos;
using DeliveryInternation2._0.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryInternation2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPost("addDish")]
        public async Task<IActionResult> AddDish(AddDishDto dish)
        {
            try
            {
                var addDish = await _dishService.AddDish(dish);
                return Ok(addDish);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("getDish")]
        public async Task<IActionResult> GetConcreateDish(Guid idDish)
        {
            try
            {
                var dish = await _dishService.GetConcreteDish(idDish);
                return Ok(dish);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("getDishesByFilters")]
        public async Task<IActionResult> GetDishesByFilters(Category? category = null, bool isVegetarian = false, int pageNum = 1, Sort? sort = null)
        {
            try
            {
                var dishes = await _dishService.GetDishesByFilters(category,isVegetarian,pageNum,sort);
                return Ok(dishes);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("addRating")]
        [Authorize(Policy ="AnyAuthenticatedUser")]
        public async Task<IActionResult> AddRating(Guid idDish, int rating)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if(emailClaim != null)
            {
                try
                {
                    var dish = await _dishService.AddDishRating(emailClaim.Value, idDish, rating);
                    return Ok(dish);
                }
                catch(UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch(KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch(ArgumentException ex)
                {
                    return BadRequest(ex.Message);
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

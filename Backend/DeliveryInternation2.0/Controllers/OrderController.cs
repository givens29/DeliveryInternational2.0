using System.Net;
using System.Security.Claims;
using DeliveryInternation2._0.Applications.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryInternation2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("createOrder")]
        [Authorize("AnyAuthenticatedUser")]
        public async Task<IActionResult> CreateOrder(string address)
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var order = await _orderService.CreateOrder(emailClaim.Value, address);
                    return Ok(order);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(ex.Message);
                }
                catch (InvalidOperationException ex)
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

        [HttpGet("getConcreateOrder")]
        [Authorize("AnyAuthenticatedUser")]
        public async Task<IActionResult> GetConcreateOrder(Guid idOrder)
        {
            try
            {
                var order = await _orderService.GetConcreateOrder(idOrder);
                return Ok(order);
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

        [HttpGet("getListOrders")]
        [Authorize("AnyAuthenticatedUser")]
        public async Task<IActionResult> GetListOrders()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                try
                {
                    var orders = await _orderService.GetListOrders(emailClaim.Value);
                    return Ok(orders);
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

        [HttpPut("confirmDelivery")]
        [Authorize("AnyAuthenticatedUser")]
        public async Task<IActionResult> ConfirmDelivery(Guid idOrder)
        {
            try
            {
                var delivery = await _orderService.ConfirmDelivery(idOrder);
                return Ok(delivery);
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
    }
}

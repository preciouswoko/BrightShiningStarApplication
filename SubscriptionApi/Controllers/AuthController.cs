using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionApi.Abstraction;
using SubscriptionApi.Dtos.Request;
using SubscriptionApi.Models;

namespace SubscriptionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _authService.Login(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateService([FromBody] CreateServiceRequest request)
        {
            // Validate the input  
            if ( string.IsNullOrEmpty(request.ServiceName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Invalid input data." });
            }

            try
            {
                // Call the service to create a new service  
                string serviceId = _authService.CreateService(request);

                // Return the service ID as a success response  
                return Ok(new { ServiceId = serviceId });
            }
            catch (InvalidOperationException ex)
            {
                // Handle the case where the service ID already exists  
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle unexpected errors  
                return StatusCode(500, new { message = "An error occurred while creating the service.", details = ex.Message });
            }
        }
    }
}



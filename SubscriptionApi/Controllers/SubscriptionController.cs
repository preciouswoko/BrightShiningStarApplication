using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionApi.Abstraction;
using SubscriptionApi.Dtos.Request;

namespace SubscriptionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] SubscribeRequest request)
        {
            var result = _subscriptionService.Subscribe(request);
            if (result == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            return Ok(result);
        }

        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe([FromBody] UnsubscribeRequest request)
        {
            var result = _subscriptionService.Unsubscribe(request);
            if (result == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }

            return Ok(new { message = "User unsubscribed successfully" });
        }

        [HttpPost("status")]
        public IActionResult CheckStatus([FromBody] CheckStatusRequest request)
        {
            var status = _subscriptionService.CheckStatus(request);
            if (status == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(status);
        }
    }

}

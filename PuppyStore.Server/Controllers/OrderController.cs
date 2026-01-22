using Microsoft.AspNetCore.Mvc;
using PuppyStore.Server.Services;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IEmailService _email;

        public OrderController(IEmailService email)
        {
            _email = email;
        }

        [HttpPost("send-confirmation")]
        public async Task<IActionResult> SendConfirmation([FromBody] OrderEmailRequest req)
        {
            await _email.SendOrderConfirmationAsync(req.UserEmail, req.Body);
            return Ok();
        }
    }

    public class OrderEmailRequest
    {
        public string UserEmail { get; set; } = "";
        public string Body { get; set; } = "";
    }
}

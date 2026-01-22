using Microsoft.AspNetCore.Mvc;
using PuppyStore.Shared.Models;
using PuppyStore.Server.Services;

namespace PuppyStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _email;

        public ContactController(IEmailService email)
        {
            _email = email;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send(ContactForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid message format.");

            try
            {
                await _email.SendContactAsync(form);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email failed: " + ex.Message);
                return StatusCode(500, "Email failed to send.");
            }
        }
    }
}

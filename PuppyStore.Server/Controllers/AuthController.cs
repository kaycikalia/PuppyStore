using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStore.Server.Data;
using PuppyStore.Shared.Models;


namespace PuppyStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly PuppyStoreDbContext _context;

        public AuthController(PuppyStoreDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<PSUser>> Login([FromBody] LoginRequest req)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == req.Email && u.Password == req.Password);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }

        // POST: api/auth/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] PSUser user)
        {
            // 1️⃣ Check if email already exists
            var existing = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existing != null)
            {
                return BadRequest("This email is already registered.");
            }

            // 2️⃣ Save new user
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(true);
        }

    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}

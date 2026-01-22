using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStore.Server.Data;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetStoreProxyController : ControllerBase
    {
        private readonly PuppyStoreDbContext _db;

        public PetStoreProxyController(PuppyStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] PSUser request)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == request.Email);

            if (exists)
                return BadRequest("User already exists");

            _db.Users.Add(request);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<PSUser>> Login([FromBody] PSUser request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Email and password are required");

            // MATCH EMAIL + PASSWORD TOGETHER
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Incorrect email or password");

            return Ok(new
            {
                user.Email,
                user.FirstName,
                user.LastName
            });
        }


    }
}

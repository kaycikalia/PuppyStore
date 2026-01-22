using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStore.Server.Data;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly PuppyStoreDbContext _context;

        public CartController(PuppyStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/cart/5  (get all cart items for userId = 5)
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<CartItem>>> GetCart(int userId)
        {
            return await _context.CartItem
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            // Check if the item already exists so we increase quantity instead
            var existing = await _context.CartItem
                .FirstOrDefaultAsync(c => c.UserId == item.UserId && c.PuppyId == item.PuppyId);

            if (existing != null)
            {
                return Ok();
            }
            else
            {
                _context.CartItem.Add(item);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/cart/remove/5/12   (userId=5, puppyId=12)
        [HttpDelete("remove/{userId}/{puppyId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int puppyId)
        {
            var item = await _context.CartItem
                .FirstOrDefaultAsync(c => c.UserId == userId && c.PuppyId == puppyId);

            if (item == null)
                return NotFound();

            _context.CartItem.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/cart/clear/5
        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var items = _context.CartItem.Where(c => c.UserId == userId);

            _context.CartItem.RemoveRange(items);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

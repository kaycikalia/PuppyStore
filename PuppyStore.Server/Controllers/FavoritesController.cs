using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppyStore.Server.Data;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly PuppyStoreDbContext _db;

        public FavoritesController(PuppyStoreDbContext db)
        {
            _db = db;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FavoriteItem>>> GetFavorites(int userId)
        {
            return await _db.FavoriteItems
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(FavoriteItem item)
        {
            _db.FavoriteItems.Add(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{userId}/{puppyId}")]
        public async Task<IActionResult> RemoveFavorite(int userId, int puppyId)
        {
            var fav = await _db.FavoriteItems
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PuppyId == puppyId);

            if (fav == null) return NotFound();

            _db.FavoriteItems.Remove(fav);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}

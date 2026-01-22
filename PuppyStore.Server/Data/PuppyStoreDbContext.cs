using Microsoft.EntityFrameworkCore;
using PuppyStore.Shared;
using PuppyStore.Shared.Models;

namespace PuppyStore.Server.Data
{
    public class PuppyStoreDbContext : DbContext
    {
        public PuppyStoreDbContext(DbContextOptions<PuppyStoreDbContext> options)
            : base(options)
        {
        }
    

        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<PSUser> Users { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }


    }
}

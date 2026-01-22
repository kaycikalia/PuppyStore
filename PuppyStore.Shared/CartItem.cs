using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppyStore.Shared.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }     // Primary key

        public int UserId { get; set; }         // Which user owns this cart item
        public int PuppyId { get; set; }        // Which puppy is in the cart

        public int Quantity { get; set; } = 1;  // How many of this puppy (usually 1)

        // Optional but nice to have:
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}


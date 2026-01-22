using PuppyStore.Shared.Models;
using System.Net.Http.Json;

namespace PuppyStore.Client.Services
{
    public class CartService
    {
        private readonly HttpClient _http;
        private readonly UserService _userService;
        private readonly PuppiesRepository _repo;

        // Logged-in user cart (synced with DB)
        public List<CartItem> Cart { get; private set; } = new();

        // Guest cart (NOT saved to DB)
        public List<CartItem> GuestCart { get; private set; } = new();

        public List<CartItem> LastOrder { get; private set; } = new();

        public CartService(HttpClient http, UserService userService, PuppiesRepository repo)
        {
            _http = http;
            _userService = userService;
            _repo = repo;
        }

        public bool IsGuest => !_userService.IsLoggedIn;

        // -------------------------------
        // Load cart for logged-in users
        // -------------------------------
        public async Task LoadCart()
        {
            var user = await _userService.GetCurrentUser();

            if (user == null)
            {
                // Guests don't load from API
                Cart = new List<CartItem>();
                return;
            }

            Cart = await _http.GetFromJsonAsync<List<CartItem>>(
                $"api/cart/{user.Id}"
            ) ?? new();
        }

        // -------------------------------
        // Helper: get puppy from repo
        // -------------------------------
        public PuppiesRepository.Puppy? GetPuppy(int id)
        {
            return _repo.All().FirstOrDefault(p => p.NumericId == id);
        }

        // -------------------------------
        // Add to cart
        // -------------------------------
        public async Task AddToCart(int puppyId)
        {
            var user = await _userService.GetCurrentUser();

            // Guest mode
            if (user == null)
            {
                GuestCart.Add(new CartItem
                {
                    PuppyId = puppyId,
                    Quantity = 1
                });
                return;
            }

            // Logged-in user → save to DB
            var item = new CartItem
            {
                UserId = user.Id,
                PuppyId = puppyId,
                Quantity = 1
            };

            await _http.PostAsJsonAsync("api/cart/add", item);

            await LoadCart();
        }

        // -------------------------------
        // Remove item from cart
        // -------------------------------
        public async Task RemoveFromCart(int puppyId)
        {
            var user = await _userService.GetCurrentUser();

            if (user == null)
            {
                GuestCart.RemoveAll(c => c.PuppyId == puppyId);
                return;
            }

            await _http.DeleteAsync($"api/cart/remove/{user.Id}/{puppyId}");
            await LoadCart();
        }

        // -------------------------------
        // Clear cart
        // -------------------------------
        public async Task ClearCart()
        {
            var user = await _userService.GetCurrentUser();

            if (user == null)
            {
                GuestCart.Clear();
                return;
            }

            await _http.DeleteAsync($"api/cart/clear/{user.Id}");
            await LoadCart();
        }

        // -------------------------------
        // Save current cart for order receipt
        // -------------------------------
        public void PrepareOrderForReceipt()
        {
            LastOrder = (IsGuest ? GuestCart : Cart)
                .Select(c => new CartItem
                {
                    PuppyId = c.PuppyId,
                    Quantity = c.Quantity
                })
                .ToList();
        }
    }
}

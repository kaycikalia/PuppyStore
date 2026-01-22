using Blazored.LocalStorage;
using PuppyStore.Shared.Models;

namespace PuppyStore.Client.Services
{
    public class FavoritesService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly UserService _userService;

        public HashSet<string> FavoriteIds { get; private set; } = new();

        public FavoritesService(ILocalStorageService localStorage, UserService userService)
        {
            _localStorage = localStorage;
            _userService = userService;
        }

        // ⭐ Identify the correct localStorage key
        private async Task<string> GetStorageKey()
        {
            var user = await _userService.GetCurrentUser();
            return user == null ? "guestFavorites" : $"favorites_{user.Id}";
        }

        // ⭐ Load favorites (guest or user)
        public async Task LoadFavorites()
        {
            string key = await GetStorageKey();
            FavoriteIds = await _localStorage.GetItemAsync<HashSet<string>>(key)
                        ?? new HashSet<string>();
        }

        // ⭐ Save favorites
        private async Task SaveFavorites()
        {
            string key = await GetStorageKey();
            await _localStorage.SetItemAsync(key, FavoriteIds);
        }

        // ⭐ ADD Favorite (new method for login merge)
        public async Task AddFavorite(string puppyId)
        {
            FavoriteIds.Add(puppyId);
            await SaveFavorites();
        }

        // ❤️ Toggle Favorite
        public async Task ToggleFavorite(string puppyId)
        {
            if (FavoriteIds.Contains(puppyId))
                FavoriteIds.Remove(puppyId);
            else
                FavoriteIds.Add(puppyId);

            await SaveFavorites();
        }

        // ⭐ MERGE guest → user favorites after login
        public async Task MergeGuestFavoritesIntoUser()
        {
            var user = await _userService.GetCurrentUser();
            if (user == null) return; // No login, nothing to merge

            // Load guest favorites
            var guestFavs = await _localStorage.GetItemAsync<HashSet<string>>("guestFavorites")
                            ?? new HashSet<string>();

            if (guestFavs.Any())
            {
                // Load user favorites
                string userKey = $"favorites_{user.Id}";
                var userFavs = await _localStorage.GetItemAsync<HashSet<string>>(userKey)
                               ?? new HashSet<string>();

                // Merge!
                foreach (var fav in guestFavs)
                    userFavs.Add(fav);

                // Save
                await _localStorage.SetItemAsync(userKey, userFavs);

                // Update memory
                FavoriteIds = userFavs;

                // Clear guest favorites
                await _localStorage.RemoveItemAsync("guestFavorites");
            }
        }

        // ❌ Clear all favorites (logout)
        public async Task ClearFavorites()
        {
            FavoriteIds.Clear();

            // Clear guest favorites
            await _localStorage.RemoveItemAsync("guestFavorites");

            // Clear user favorites
            var user = await _userService.GetCurrentUser();
            if (user != null)
                await _localStorage.RemoveItemAsync($"favorites_{user.Id}");
        }

        // Initialize on first run
        public async Task Initialize()
        {
            await LoadFavorites();
        }
    }
}

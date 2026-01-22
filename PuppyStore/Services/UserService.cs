using Blazored.SessionStorage;
using PuppyStore.Shared.Models;
using System.Net.Http.Json;

namespace PuppyStore.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _http;
        private readonly ISessionStorageService _session;

        private PSUser? _currentUser;

        public UserService(HttpClient http, ISessionStorageService session)
        {
            _http = http;
            _session = session;
        }

        public PSUser? CurrentUser => _currentUser;

        public bool IsLoggedIn => _currentUser != null;

        public async Task<PSUser?> GetCurrentUser()
        {
            if (_currentUser != null)
                return _currentUser;

            _currentUser = await _session.GetItemAsync<PSUser>("currentUser");
            return _currentUser;
        }

        public async Task<PSUser?> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login",
                new { Email = email, Password = password });

            if (!response.IsSuccessStatusCode)
                return null;

            var user = await response.Content.ReadFromJsonAsync<PSUser>();

            if (user != null)
            {
                _currentUser = user;
                await _session.SetItemAsync("currentUser", user);
            }

            return user;
        }

        public async Task Logout()
        {
            _currentUser = null;
            await _session.RemoveItemAsync("currentUser");
        }

        public async Task<bool> CreateUserAsync(PSUser user)
        {
            var response = await _http.PostAsJsonAsync("api/auth/create", user);

            if (response.IsSuccessStatusCode)
                return true;

            // If the email already exists, return false
            return false;
        }


        public class LoginRequest
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
        }
    }
}

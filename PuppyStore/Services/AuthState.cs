using System;
using PuppyStore.Shared.Models;

namespace PuppyStore.Client.Services
{
    public class AuthState
    {
        // Fires whenever login/logout happens so the UI updates
        public event Action? OnChange;

        // Whether the user is logged in
        public bool IsLoggedIn { get; private set; } = false;

        // Currently logged-in user
        public PSUser? CurrentUser { get; private set; }

        // Called when a user logs in
        public void SetUser(PSUser user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
            NotifyStateChanged();
        }

        // Called when a user logs out
        public void ClearUser()
        {
            CurrentUser = null;
            IsLoggedIn = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

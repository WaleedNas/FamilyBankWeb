using Blazored.SessionStorage;

namespace FamilyBankWeb.Services
{
    public class AppState
    {
        private readonly ISessionStorageService _sessionStorage;

        public AppState(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public bool IsAuthenticated { get; private set; }

        public event Action OnChange;
        public async Task LoadState()
        {
            IsAuthenticated = await _sessionStorage.GetItemAsync<bool>("IsAuth");
        }

        public async Task SaveState()
        {
            await _sessionStorage.SetItemAsync("IsAuth", IsAuthenticated);
        }

        public async void SetIsAuthenticated(bool isAuthenticated)
        {
            IsAuthenticated = isAuthenticated;
            await SaveState();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}

using FamilyBankWeb.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace FamilyBankWeb.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionStorageService _sessionStorage;
        private readonly NavigationManager _navigationManager;
        private readonly AppState appState;

        public AuthService(IHttpClientFactory clientFactory, ISessionStorageService sessionStorage,
            NavigationManager navigationManager, AppState appState)
        {
            _clientFactory = clientFactory;
            _sessionStorage = sessionStorage;
            _navigationManager = navigationManager;
            this.appState = appState;
        }

        public async Task<bool> AuthenticateAsync(UserLoginModel userLogin)
        {

            var client = _clientFactory.CreateClient("Api");
            var response = await client.PostAsJsonAsync("Login", userLogin);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (tokenResponse != null && !string.IsNullOrWhiteSpace(tokenResponse.token))
                {

                    await _sessionStorage.SetItemAsync("authToken", tokenResponse.token);
                    await _sessionStorage.SetItemAsync("user", tokenResponse.user);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenResponse.token);
                    appState.SetIsAuthenticated(true);
                    return true;
                }
            }

            return false;
        }

        public async Task Logout()
        {

            var client = _clientFactory.CreateClient("Api");
            await _sessionStorage.RemoveItemAsync("authToken");
            await _sessionStorage.RemoveItemAsync("user");
            client.DefaultRequestHeaders.Authorization = null;
            appState.SetIsAuthenticated(false);
            _navigationManager.NavigateTo("/signin");

        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            bool IsAuth = !string.IsNullOrEmpty(token);
            return IsAuth;
        }
    }
}

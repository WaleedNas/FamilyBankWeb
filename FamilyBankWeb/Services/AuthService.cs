using FamilyBankWeb.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
using FamilyBankWeb.Services;
using Microsoft.AspNetCore.DataProtection;

namespace FamilyBankWeb.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ISessionStorageService _sessionStorage;
        private readonly NavigationManager _navigationManager;
        private readonly CustomAuthStateProvider authStateProvider;
        private readonly IDataProtectionProvider dataProtection;

        public AuthService(IHttpClientFactory clientFactory, ISessionStorageService sessionStorage,
            NavigationManager navigationManager, CustomAuthStateProvider authStateProvider, IDataProtectionProvider dataProtection)
        {
            _clientFactory = clientFactory;
            _sessionStorage = sessionStorage;
            _navigationManager = navigationManager;
            this.authStateProvider = authStateProvider;
            this.dataProtection = dataProtection;
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
                    var protector = dataProtection.CreateProtector("TheProtector");
                    string protectedToken = protector.Protect(tokenResponse.token);
                    await _sessionStorage.SetItemAsync("authToken", protectedToken);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenResponse.token);
                    await authStateProvider.GetAuthenticationStateAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task Logout()
        {

            var client = _clientFactory.CreateClient("Api");
            await _sessionStorage.RemoveItemAsync("authToken");
            await _sessionStorage.RemoveItemAsync("role");
            await _sessionStorage.RemoveItemAsync("accountID");
            client.DefaultRequestHeaders.Authorization = null;
            _navigationManager.NavigateTo("/signin");
            await authStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            bool IsAuth = !string.IsNullOrEmpty(token);
            return IsAuth;
        }
    }
}

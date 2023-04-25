using Blazored.SessionStorage;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace FamilyBankWeb.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;

        public CustomAuthStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _sessionStorage.GetItemAsync<string>("authToken");
            //string roleValue = await _sessionStorage.GetItemAsync<string>("role");

            var identity = new ClaimsIdentity();
            if (token?.Length > 0)
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            }

            var user = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch(base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;

            }
            return Convert.FromBase64String(base64);
        }

    }
}

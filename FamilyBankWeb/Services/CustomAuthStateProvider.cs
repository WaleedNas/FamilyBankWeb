using Blazored.SessionStorage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace FamilyBankWeb.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly IDataProtectionProvider dataProtection;

        public CustomAuthStateProvider(ISessionStorageService sessionStorage, IDataProtectionProvider dataProtection)
        {
            _sessionStorage = sessionStorage;
            this.dataProtection = dataProtection;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string protectedToken = await _sessionStorage.GetItemAsync<string>("authToken");
            string protectedRoleValue = await _sessionStorage.GetItemAsync<string>("role");
            var identity = new ClaimsIdentity();
            if (protectedToken?.Length > 0)
            {
                var protector = dataProtection.CreateProtector("TheProtector");
                string token = protector.Unprotect(protectedToken);
                string roleValue = "";
                try
                {
                    roleValue = protector.Unprotect(protectedRoleValue);

                } catch (Exception ex) { }
                identity = new ClaimsIdentity(ParseClaimsFromJwt(token, roleValue), "jwt");

            }

            var user = new ClaimsPrincipal(identity);

            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt, string roleValue)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            //var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            if (!string.IsNullOrEmpty(roleValue))
            {
                claims.Add(new Claim(ClaimTypes.Role, roleValue));

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            
            return claims;
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

using FamilyBankWeb.Models;
using System.Net.Http;

namespace FamilyBankWeb.Data
{
    public class AccountData
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public AccountData(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> CreateAccount(int userID, double balance)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync($"/Accounts/{userID}", balance);
        }
        public async Task<HttpResponseMessage> CreateAccountUser(AccountUserModel accountUser)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync("/Accounts/User", accountUser);
        }
    }
}

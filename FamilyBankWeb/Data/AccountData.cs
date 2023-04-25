using FamilyBankWeb.Models;
using System.Net.Http;
using System.Text.Json;

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

        public async Task<List<AccountModel>> GetAccountsForUser(int id)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Accounts/User/{id}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<AccountModel>>(json);
            return accounts;
        }
    }
}

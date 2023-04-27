using FamilyBankWeb.Models;
using System.Text.Json;

namespace FamilyBankWeb.Data
{
    public class TransactionData
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionData(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TransactionModel> GetTransaction(int transID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Transactions/{transID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var trans = JsonSerializer.Deserialize<TransactionModel>(json);
            return trans;
        }

        public async Task<TransactionModel> GetTransactionsForAccount(int accountID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Transactions/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var trans = JsonSerializer.Deserialize<TransactionModel>(json);
            return trans;
        }
    }
}

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

        public async Task<List<TransactionModel>> GetTransactionsForAccount(int accountID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Transactions/Account/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var trans = JsonSerializer.Deserialize<List<TransactionModel>>(json);
            return trans;
        }
        public async Task<List<TransactionModel>> GetUserTransactionsForAccount(int userID, int accountID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Transactions/{userID}/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var trans = JsonSerializer.Deserialize<List<TransactionModel>>(json);
            return trans;
        }

        public async Task<HttpResponseMessage> CreateTransaction(TransactionModel transaction)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync("/Transactions", transaction);
        }
        public async Task<HttpResponseMessage> CreateScheduledTransaction(ScheduledTransactions scheduledTransactions)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync("/Transactions/Scheduled", scheduledTransactions);
        }

        public async Task<List<ScheduledTransactions>> GetScheduledTransactionsForAccount(int accountID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Transactions/Scheduled/Account/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var trans = JsonSerializer.Deserialize<List<ScheduledTransactions>>(json);
            return trans;
        }

        public async Task<HttpResponseMessage> DeleteScheduledTransaction(int id)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.DeleteAsync($"/Transactions/Scheduled/{id}");
        }

        public async Task<HttpResponseMessage> CreateTransfer(TransferModel transferModel)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync("Transfers", transferModel);
        }

        public async Task<List<TransferModel>> GetTransfers(int accountID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Transfers/Account/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var trans = JsonSerializer.Deserialize<List<TransferModel>>(json);
            return trans;
        }

    }
}

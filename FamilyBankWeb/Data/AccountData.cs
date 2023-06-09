﻿using FamilyBankWeb.Models;
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
            var response = await client.PostAsJsonAsync($"/Accounts/{userID}", balance);

            var accountJson = await response.Content.ReadAsStringAsync();
            var account = JsonSerializer.Deserialize<AccountModel>(accountJson);

            ScheduledTransactions schedTransaction = new ScheduledTransactions()
            {
                accountID = account.accountID,
                userID = userID,
                debit = false,
                amount = 7000,
                title = "Salary",
                description = "Monthly Salary",
                transactionDate = DateTime.Now,
                transactionFrequency = 4,
                isCompleted = false
            };

            await client.PostAsJsonAsync($"/Transactions/Scheduled", schedTransaction);
            return response;
        }
        public async Task<HttpResponseMessage> CreateAccountUser(AccountUserModel accountUser)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync("/Accounts/User", accountUser);
        }

        public async Task<List<AccountUserModel>> GetAccountUsers(int id)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Accounts/User/{id}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var accountUsers = JsonSerializer.Deserialize<List<AccountUserModel>>(json);
            return accountUsers;
        }

        public async Task<AccountModel> GetAccount(int id)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Accounts/{id}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var account = JsonSerializer.Deserialize<AccountModel>(json);
            return account;
        }

        public async Task<double> GetBalance(int accountID, int userID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Users/Allowance/{userID}/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var account = JsonSerializer.Deserialize<double>(json);
            return account;
        }

        public async Task<string> OTP (string mobileNum)
        {
            
                var client = _httpClientFactory.CreateClient("Api");
                var response = await client.PostAsJsonAsync("/Accounts/OTP", mobileNum);
            return await response.Content.ReadFromJsonAsync<string>();
                
        }
    }
}

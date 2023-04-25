using FamilyBankWeb.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Blazored.SessionStorage;
using System;

namespace FamilyBankWeb.Data
{
    public class UserData : IUserData
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISessionStorageService _sessionStorage;

        public UserData(IHttpClientFactory httpClientFactory, ISessionStorageService sessionStorage)
        {
            _httpClientFactory = httpClientFactory;
            _sessionStorage = sessionStorage;
        }
        public async Task<UserModel> GetUser(int id)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Users/{id}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserModel>(json);
            return user;
        }

        public async Task<HttpResponseMessage> CreateUser(UserModel userModel)
        {
            var client = _httpClientFactory.CreateClient("Api");
            return await client.PostAsJsonAsync("Users", userModel);
        }

        public async Task<int> GetIdFromLogin(UserLoginModel userLogin)
        {

            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.PostAsJsonAsync("Login", userLogin);
            if (response.IsSuccessStatusCode)
            {
                var userResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (userResponse != null && !string.IsNullOrWhiteSpace(userResponse.token))
                {
                    return userResponse.user.userID;
                }
            }
            return -1;
        }

        public Task<UserModel> GetUserFromContext(string claim)
        {
            string claimValue = claim;
            string[] parts = claimValue?.Split(":");
            int id = parts != null && parts.Length > 1 ? int.Parse(parts[1].Trim()) : 0;
            return GetUser(id);
        }

        public async Task<List<UserModel>> GetUsersFromAccount(int id)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Users/Account/{id}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserModel>>(json);
            return users;
        }
    }
}

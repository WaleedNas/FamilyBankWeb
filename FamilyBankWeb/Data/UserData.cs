using FamilyBankWeb.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Blazored.SessionStorage;

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
            var response = await client.GetAsync($"{id}");
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

        public async Task<bool> IsUserLoggedIn()
        {
            bool flag = false;
            var result = await _sessionStorage.GetItemAsync<string>("authToken");
            if (result != null)
            {
                flag = true;
            }
            return flag;
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
    }
}

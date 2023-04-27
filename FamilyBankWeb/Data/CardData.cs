using FamilyBankWeb.Models;
using System.Text.Json;

namespace FamilyBankWeb.Data
{
    public class CardData
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CardData(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CardModel> GetCard(int cardID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Cards/{cardID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var card = JsonSerializer.Deserialize<CardModel>(json);
            return card;
        }

        public async Task<CardModel> GetAccountCard(int accountID)
        {
            var client = _httpClientFactory.CreateClient("Api");
            var response = await client.GetAsync($"Cards/Account/{accountID}");
            response.EnsureSuccessStatusCode(); // response gets the json file
            var json = await response.Content.ReadAsStringAsync();
            var card = JsonSerializer.Deserialize<CardModel>(json);
            return card;
        }
    }
}

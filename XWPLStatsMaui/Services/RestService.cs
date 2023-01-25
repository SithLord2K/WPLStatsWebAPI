using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public class RestService : IRestService
    {
        static HttpClient client;
        static readonly string BaseURL = "https://wileysoft.codersden.com";

        public RestService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL),
            };
            client.DefaultRequestHeaders.Add("APIKey", "TDLoRo8deL0Bd9p6HfFMNONvtWAlz76YFXy3HIKMkgbSTA3Gkhllrle1a5FPiTkUjAuHcSicguMOQMUO7OuGj6nJg5h3VXc8h5gBrx2YRftwc7NRGl2R4cqv22aRJPnB");

        }

        static async Task<T> GetAsync<T>(string url, string key, int mins, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                var data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw;
            }
        }

        public Task<List<Players>> GetAllPlayers() =>
            GetAsync<List<Players>>("/api/Players", "getplayers", 30);
 
        public Task<List<Players>> GetSinglePlayer(int id) =>
            GetAsync<List<Players>>($"/api/Players/{id}", $"getsingleplayer/{id}", 20);

        public async Task SavePlayer(Players player)
        {
            Uri uri = new("https://wileysoft.codersden.com/api/Players");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Players>(player)
                
            };
            Barrel.Current.EmptyAll();
            _ = await client.SendAsync(message);
        }

        public async Task DeletePlayer(int id)
        {
            Uri uri = new($"https://wileysoft.codersden.com/api/Players/{id}");
            HttpRequestMessage message = new(HttpMethod.Delete, uri)
            { 
                Content = JsonContent.Create<int>(id)
            };
            _ = await client.SendAsync(message);
        }

         public async Task<List<int>> GetDistinctPlayer()
        {
            List<Players> players = new();
            Uri uri = new("https://wileysoft.codersden.com/api/Players");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                players = System.Text.Json.JsonSerializer.Deserialize<List<Players>>(content);
            }
            var playerId = players.Select(x => x.Id).Distinct().ToList();
            return players.Select(x => x.Id).Distinct().ToList();
        }

        public async Task<List<Players>> GetAllBySingleId(int id)
        {
            List<Players> players = new();
            Uri uri = new($"https://wileysoft.codersden.com/api/Players/{id}");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                players = System.Text.Json.JsonSerializer.Deserialize<List<Players>>(content);
            }
            return players;
        }

        public Task<IEnumerable<Weeks>> GetAllWeeks([Optional]bool forceRefresh) =>
            GetAsync<IEnumerable<Weeks>>("/api/Weeks", "getallweeks", 20, forceRefresh);
 
        public async Task AddWeeks(Weeks weeks)
        {
            Uri uri = new("https://wileysoft.codersden.com/api/Weeks");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Weeks>(weeks)
            };
            Barrel.Current.EmptyAll()
            _ = await client.SendAsync(message);
        }

        public Task UpdateWeeks(Weeks weeks)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveWeeks(int id)
        {
            Uri uri = new($"https://wileysoft.codersden.com/api/Weeks/{id}");
            HttpRequestMessage message = new(HttpMethod.Delete, uri)
            {
                Content = JsonContent.Create<int>(id)
            };
            _ = await client.SendAsync(message);
        }

        public Task<List<TeamDetails>> GetTeamDetails() =>
                 GetAsync<List<TeamDetails>>($"/api/TeamDetails", "getteamdetails", 120, true);
    }
}

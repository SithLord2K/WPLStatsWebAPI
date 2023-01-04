using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public class RestService : IRestService
    {
        static HttpClient client;
        static string BaseURL = "https://wileysoft.codersden.com";

        public RestService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL),
            };
            client.DefaultRequestHeaders.Add("APIKey", "TDLoRo8deL0Bd9p6HfFMNONvtWAlz76YFXy3HIKMkgbSTA3Gkhllrle1a5FPiTkUjAuHcSicguMOQMUO7OuGj6nJg5h3VXc8h5gBrx2YRftwc7NRGl2R4cqv22aRJPnB");


        }

        static async Task<T> GetAsync<T>(string url, string key, int mins = 120, bool forceRefresh = false)
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
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw;
            }
        }

        public Task<List<Players>> GetAllPlayers() =>
            GetAsync<List<Players>>("/api/Players", "getplayers");
        //{
        //    List<Players> players = new();
        //    Uri uri = new("https://wileysoft.codersden.com/api/Players");
        //    HttpResponseMessage response = await client.GetAsync(uri);
        //    //HttpResponseHeaders headers = response.Headers;
        //    //string header = headers.GetValues("Cache-Control").FirstOrDefault();
        //    //await Shell.Current.DisplayAlert("Cache Header", header, "Ok");
        //    if(response.IsSuccessStatusCode)
        //    {
        //        string content = await response.Content.ReadAsStringAsync();
        //        players = System.Text.Json.JsonSerializer.Deserialize<List<Players>>(content);
        //    }
            
        //    return players;
        //}

        public async Task<List<Players>> GetSinglePlayer(int id)
        {
            List<Players> fullPlayer = new();
            Players player = new();
            Uri uri = new($"https://wileysoft.codersden.com/api/Players/{id}");
            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.IsSuccessStatusCode) 
            {
                string content = await response.Content.ReadAsStringAsync();
                fullPlayer = System.Text.Json.JsonSerializer.Deserialize<List<Players>>(content);
                
            }
            return fullPlayer;
        }

        public async Task SavePlayer(Players player)
        {
            Uri uri = new("https://wileysoft.codersden.com/api/Players");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Players>(player)
            };
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

        public async Task<IEnumerable<Weeks>> GetAllWeeks()
        {
            List<Weeks> weeks = new();
            Uri uri = new("https://wileysoft.codersden.com/api/Weeks");
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                weeks = System.Text.Json.JsonSerializer.Deserialize<List<Weeks>>(content);
            }

            return weeks;
        }

        public async Task AddWeeks(Weeks weeks)
        {
            Uri uri = new("https://wileysoft.codersden.com/api/Weeks");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Weeks>(weeks)
            };
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
    }
}

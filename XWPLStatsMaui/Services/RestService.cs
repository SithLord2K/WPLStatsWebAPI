﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public class RestService : IRestService
    {
        readonly HttpClient _client;
        public RestService()
        {
            _client = new HttpClient();
        }
        public async Task<List<Players>> GetAllPlayers()
        {
            List<Players> players = new();
            Uri uri = new("https://wileysoft.codersden.com/api/Players");
            HttpResponseMessage response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                players = JsonSerializer.Deserialize<List<Players>>(content);
            }
            
            return players;
        }

        public async Task<Players> GetSinglePlayer(int id)
        {
            Players player = new();
            Uri uri = new($"https://wileysoft.codersden.com/api/Players/{id}");
            HttpResponseMessage response = await _client.GetAsync(uri);
            if(response.IsSuccessStatusCode) 
            {
                string content = await response.Content.ReadAsStringAsync();
                player = JsonSerializer.Deserialize<Players>(content);
            }
            return player;
        }

        public async Task SavePlayer(Players player)
        {
            Uri uri = new("https://wileysoft.codersden.com/api/Players");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Players>(player)
            };

            HttpResponseMessage response = await _client.SendAsync(message);
        }
        public Task DeletePlayer(int id)
        {
            throw new NotImplementedException();
        }

 
        public async Task<List<int>> GetDistinctPlayer()
        {
            List<Players> players = new();
            Uri uri = new("https://wileysoft.codersden.com/api/Players");
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                players = JsonSerializer.Deserialize<List<Players>>(content);
            }
            var playerId = players.Select(x => x.Id).Distinct().ToList();
            return players.Select(x => x.Id).Distinct().ToList();
        }

        public async Task<List<Players>> GetAllBySingleId(int id)
        {
            List<Players> players = new();
            Uri uri = new($"https://wileysoft.codersden.com/api/Players/{id}");
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                players = JsonSerializer.Deserialize<List<Players>>(content);
            }
            return players;
        }

        public async Task<IEnumerable<Weeks>> GetAllWeeks()
        {
            List<Weeks> weeks = new();
            Uri uri = new("https://wileysoft.codersden.com/api/Weeks");
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                weeks = JsonSerializer.Deserialize<List<Weeks>>(content);
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

            HttpResponseMessage response = await _client.SendAsync(message);
        }

        public Task UpdateWeeks(Weeks weeks)
        {
            throw new NotImplementedException();
        }

        public Task RemoveWeeks(Weeks weeks)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XWPLStats.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace XWPLStats.Services
{
    public class PlayerHelpers
    {

        readonly IRestService restService = new RestService();
        public async Task<List<Players>> ConsolidatePlayer()
        {
            var pList = new List<Players>();


            //var players = await playerService.GetDistinctPlayerId();
            var players = await restService.GetDistinctPlayer();

            if (players.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var item in players)
                {
                    Players playerTotals = new();

                    var getPlayerData = await restService.GetAllBySingleId(item); //playerService.GetAllBySingleId(item);
                    if (getPlayerData != null)
                    {
                        foreach (var single in getPlayerData)
                        {
                            playerTotals.Id = single.Id;
                            playerTotals.Name = single.Name;
                            playerTotals.GamesWon += single.GamesWon;
                            playerTotals.GamesLost += single.GamesLost;
                            playerTotals.GamesPlayed = playerTotals.GamesWon + playerTotals.GamesLost;
                            playerTotals.Average = Decimal.Round((decimal)(playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed) * 100, 2);
                            playerTotals.WeekNumber = single.WeekNumber;
                        }
                    }

                    pList.Add(playerTotals);
                }

            }
            return pList;
        }

        public async Task<Players> GetPlayerDetails(int id)
        {
            Players playerTotals = new();
            var getPlayerData = await restService.GetAllBySingleId(id);//playerService.GetAllBySingleId(id);
            if (getPlayerData != null)
            {
                foreach (var single in getPlayerData)
                {
                    playerTotals.Id = single.Id;
                    playerTotals.Name = single.Name;
                    playerTotals.GamesWon += single.GamesWon;
                    playerTotals.GamesLost += single.GamesLost;
                    playerTotals.GamesPlayed = playerTotals.GamesWon + playerTotals.GamesLost;
                    playerTotals.Average = Decimal.Round((decimal)(playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed) * 100, 2);
                    playerTotals.WeekNumber = getPlayerData.Count;
                }
            }

            
            return playerTotals;
        }
        
    }
}


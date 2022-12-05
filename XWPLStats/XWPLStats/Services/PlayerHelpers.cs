using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XWPLStats.Models;

namespace XWPLStats.Services
{
    public class PlayerHelpers
    {
        private List<Players> pList = new List<Players>();
        private Players playerTotals;
        IPlayerService playerService = new PlayerService();
        public async Task<List<Players>> ConsolidatePlayer()
        {
            playerTotals = new Players();
            var players = await playerService.GetAllPlayersAsync();

            if (players.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var item in players)
                {
                    
                    var getPlayerData = await playerService.GetAllBySingleId(item.Id);

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
                        }
                    }
                    else
                    {
                        break;
                    }

                    pList.Add(playerTotals);
                    playerTotals = new Players();
                }

            }
            return pList;
        }
    }
}


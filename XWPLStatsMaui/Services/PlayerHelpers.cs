using XWPLStats.Models;

namespace XWPLStats.Services
{
    public class PlayerHelpers
    {

        readonly IRestService restService = new RestService();
        public async Task<List<Players>> ConsolidatePlayer()
        {
            var pList = new List<Players>();

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

                    var getPlayerData = await restService.GetAllBySingleId(item);
                    if (getPlayerData != null)
                    {
                        playerTotals.Id = getPlayerData.FirstOrDefault().Id;
                        playerTotals.Name = getPlayerData.FirstOrDefault().Name;
                        playerTotals.GamesWon = getPlayerData.Sum(x => x.GamesWon);
                        playerTotals.GamesLost = getPlayerData.Sum(y => y.GamesLost);
                        playerTotals.GamesPlayed = playerTotals.GamesWon + playerTotals.GamesLost;
                        playerTotals.Average = Decimal.Round(((decimal)playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed) * 100, 2);
                        playerTotals.WeekNumber = getPlayerData.Count;
                    }

                    pList.Add(playerTotals);
                }

            }
            return pList;
        }

        public async Task<Players> GetPlayerDetails(int id)
        {
            Players playerTotals = new();
            var getPlayerData = await restService.GetAllBySingleId(id);
            if (getPlayerData != null)
            {
                playerTotals.Id = getPlayerData.FirstOrDefault().Id;
                playerTotals.Name = getPlayerData.FirstOrDefault().Name;
                playerTotals.GamesWon = getPlayerData.Sum(x => x.GamesWon);
                playerTotals.GamesLost = getPlayerData.Sum(y => y.GamesLost);
                playerTotals.GamesPlayed = playerTotals.GamesWon + playerTotals.GamesLost;
                playerTotals.Average = Decimal.Round(((decimal)playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed) * 100, 2);
                playerTotals.WeekNumber = getPlayerData.Count;
            }

            return playerTotals;
        }

        public async Task<TeamStats> GetTeamStats()
        {
            List<Players> teamTotals= new();
            teamTotals = await restService.GetAllPlayers();
            TeamStats teamStats = new();
            teamStats.TotalGamesWon = teamTotals.Sum(x => x.GamesWon);
            teamStats.TotalGamesLost = teamTotals.Sum(y => y.GamesLost);
            teamStats.TotalGamesPlayed = teamStats.TotalGamesWon + teamStats.TotalGamesLost;
            teamStats.TotalAverage = Decimal.Round(((decimal)teamStats.TotalGamesWon / (decimal)teamStats.TotalGamesPlayed) * 100, 2);
            teamStats.WeeksPlayed = teamStats.TotalGamesPlayed / 25;
            return teamStats;
        }

        
        
    }
}


using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public partial class TeamStatsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<TeamStats> TeamStat { get; set; }
        
        readonly IRestService restService;
        
        public TeamStatsViewModel()
        {
            Title = "Team Statistics";
            TeamStat = new ObservableRangeCollection<TeamStats>();
            restService = new RestService();
        }

        [RelayCommand]
        async Task Refresh()
        {
            TeamStat.Clear();
            var players = await restService.GetAllPlayers();
            int gWon = 0;
            int gLost = 0;
            int gPlayed = 0;
            foreach (var player in players)
            {
                gWon += player.GamesWon;
                gLost += player.GamesLost;
                gPlayed += player.GamesWon + player.GamesLost;
            }
            decimal tAverage = decimal.Round(gWon / (decimal)gPlayed * 100, 2);

            TeamStats Team = new()
            {
                TotalGamesWon = gWon,
                TotalGamesLost = gLost,
                TotalGamesPlayed = gPlayed,
                TotalAverage = tAverage,
                WeeksPlayed = gPlayed / 25
            };

            TeamStat.Add(Team);

        }
    }
}

using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public partial class TeamStatsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<TeamStats> TeamStat { get; set; }
        public AsyncCommand RefreshCommand { get; }
        

        IPlayerService playerService;
        public TeamStatsViewModel()
        {
            Title = "Player List";
            TeamStat = new ObservableRangeCollection<TeamStats>();
            RefreshCommand = new AsyncCommand(Refresh);

            playerService = DependencyService.Get<IPlayerService>();
            
        }
        async Task Refresh()
        {
            IsBusy = true;
            TeamStat.Clear();
            var players = await playerService.GetAllPlayersAsync();
            int gWon =0;
            int gLost=0;
            int gPlayed=0;
            decimal tAverage = 0;
            foreach (var player in players)
            {
                gWon += player.GamesWon;
                gLost += player.GamesLost;
                gPlayed += player.GamesPlayed;
            }
            tAverage = Decimal.Round((decimal)(gWon / (decimal)gPlayed) * 100, 2);

            TeamStats Team = new TeamStats()
            {
                TotalGamesWon = gWon,
                TotalGamesLost = gLost,
                TotalGamesPlayed = gPlayed,
                TotalAverage = tAverage,
                WeeksPlayed = gPlayed / 25
            };
           

            TeamStat.Add(Team);


            IsBusy = false;
        }
    }
}

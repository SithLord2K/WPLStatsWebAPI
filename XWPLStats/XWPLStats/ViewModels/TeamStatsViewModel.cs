using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ObservableRangeCollection<Weeks> WeekStats { get; set; }
        public AsyncCommand RefreshCommand { get; }

        private bool _isBusy;
        IPlayerService playerService;
        IWeekService weekService;
        public TeamStatsViewModel()
        {
            Title = "Team and Week Statistics";
            TeamStat = new ObservableRangeCollection<TeamStats>();
            WeekStats = new ObservableRangeCollection<Weeks>();
            RefreshCommand = new AsyncCommand(Refresh);

            weekService = new WeekService();
            playerService = DependencyService.Get<IPlayerService>();

        }
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        async Task Refresh()
        {
            TeamStat.Clear();
            var players = await playerService.GetAllPlayersAsync();
            int gWon = 0;
            int gLost = 0;
            int gPlayed = 0;
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


            //Get Week Stats


            WeekStats.Clear();
            var weeks = await weekService.GetAllWeeksAsync();
            weeks = weeks.OrderByDescending(a => a.Id);
            WeekStats.Add((Weeks)weeks.FirstOrDefault());
            IsBusy = false;
        }
    }
}

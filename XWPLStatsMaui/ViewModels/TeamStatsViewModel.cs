using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public partial class TeamStatsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<TeamStats> TeamStat { get; set; }
        
        private bool _isBusy;
        readonly IPlayerService playerService;
        
        public TeamStatsViewModel()
        {
            Title = "Team Statistics";
            TeamStat = new ObservableRangeCollection<TeamStats>();
            playerService = new PlayerService();

        }
        public new bool IsBusy
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
        [RelayCommand]
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

        }
    }
}

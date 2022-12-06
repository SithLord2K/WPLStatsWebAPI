using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;
using XWPLStats.Views;


namespace XWPLStats.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Players> Player { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddPlayerCommand { get; }
        public AsyncCommand<Players> RemoveCommand { get; }
        public AsyncCommand<Players> SelectedCommand { get; }

        private List<Players> pList = new List<Players>();
        string name;
        int gamesWon, gamesLost, playerId, gamesPlayed, weekNumber;
        decimal average;

        public string Name { get => name; set => SetProperty(ref name, value); }
        public int PlayerId { get => playerId; set => SetProperty(ref playerId, value); }
        public int GamesWon { get => gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public int GamesPlayed { get => gamesPlayed; set => SetProperty(ref gamesPlayed, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }

        IPlayerService playerService;
        PlayerHelpers pHelper = new PlayerHelpers();
        public bool _isBusy;
        public MainPageViewModel()
        {
            Title = "Player List";
            Player = new ObservableRangeCollection<Players>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddPlayerCommand = new AsyncCommand(AddPlayer);
            RemoveCommand = new AsyncCommand<Players>(Remove);
            SelectedCommand = new AsyncCommand<Players>(Selected);

            playerService = new PlayerService();
        }
        public new bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        async Task AddPlayer()
        {
            await Shell.Current.GoToAsync($"{nameof(AddUpdatePlayer)}");
        }

        async Task Selected(Players player)
        {
            if (player == null)
                return;

            var route = $"{nameof(PlayerDetailPage)}?PlayerID={player.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Players player)
        {
            await playerService.RemovePlayer(player.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            if (Player.Count > 0)
            {
                Player.Clear();
                //Player = new ObservableRangeCollection<Players>();
            }
            Players playerTotals = new Players();
            var players = await playerService.GetAllPlayersAsync();

            if (players.Count == 0)
            {
                return;
            }
            else
            {
                //foreach (var item in players)
               // {

                    //        var getPlayerData = await playerService.GetAllBySingleId(item.Id);
                    //        if (getPlayerData != null)
                    //        {
                    //            foreach (var single in getPlayerData)
                    //            {
                    //                playerTotals.Id = single.Id;
                    //                playerTotals.Name = single.Name;
                    //                playerTotals.GamesWon += single.GamesWon;
                    //                playerTotals.GamesLost += single.GamesLost;
                    //                playerTotals.GamesPlayed = playerTotals.GamesWon + playerTotals.GamesLost;
                    //                playerTotals.Average = Decimal.Round((decimal)(playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed) * 100, 2);
                    //            }
                    //        }
                    //    pList.Add(playerTotals);
                    pList = await pHelper.ConsolidatePlayer();

                    //Player.AddRange(players);
                //}
                var sorted = pList.OrderByDescending(a => a.Average);
                Player.AddRange(sorted);
                IsBusy = false;

            }
        }
    }
}

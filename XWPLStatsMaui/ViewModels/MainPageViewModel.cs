using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;
using XWPLStats.Views;
using CommunityToolkit.Mvvm.Input;

namespace XWPLStats.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Players> Player { get; set; }

        public List<Players> pList = new();
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

        readonly IRestService restService;
        readonly PlayerHelpers pHelper = new();
  
        readonly NetworkAccess access = Connectivity.Current.NetworkAccess;
        public MainPageViewModel()
        {
            Title = "Player List";
            Player = new ObservableRangeCollection<Players>();
            restService = new RestService();
            if (access != NetworkAccess.Internet)
            {
                Shell.Current.DisplayAlert("Internet Access Required", "Please connect to the internet and restart the application.", "OK");
                App.Current.Quit();
            }
        }

        [RelayCommand]
        async Task AddPlayer()
        {
            var route = $"{nameof(AddPlayer)}";
            await Shell.Current.GoToAsync(route,true);
        }

        [RelayCommand]
        async Task Selected(Players player)
        {
            if (player == null)
                return;

            var route = $"{nameof(PlayerDetailPage)}?PlayerID={player.Id}";
            await Shell.Current.GoToAsync(route,true);
        }

        [RelayCommand]
        async Task Remove(Players player)
        {
            var removePlayer = await restService.GetAllBySingleId(player.Id);
            foreach (var item in removePlayer)
            {
                await restService.DeletePlayer(item.EntryId);
            }
            await Refresh();
        }

        [RelayCommand]
        private async Task ViewWeeks(object sender)
        {
            var player = sender;
            if (player == null)
                return;

            await Shell.Current.GoToAsync(nameof(EditPlayer),true,new Dictionary<string,object>
            {
                {"Player", player }
            });
        }

        [RelayCommand]
        async Task Refresh()
        {
            IsBusy = true;
            if (Player.Count != 0)
            {
                Player.Clear();

            }
            var players = await restService.GetAllPlayers();

            if (players.Count == 0)
            {
                return;
            }
            else
            {
                pList = await pHelper.ConsolidatePlayer();
                var sorted = pList.OrderByDescending(a => a.Average);
                Player.AddRange(sorted);

            }
            IsBusy = false;
        }
    }
}

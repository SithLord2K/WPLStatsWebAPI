using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XWPLStats.Models;
using XWPLStats.Services;
using XWPLStats.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace XWPLStats.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Players> Player { get; set; }

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

        IRestService restService;
        PlayerHelpers pHelper = new();
        public bool _isBusy;
        public MainPageViewModel()
        {
            Title = "Player List";
            Player = new ObservableRangeCollection<Players>();
            restService = new RestService();
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

        [RelayCommand]
        async Task AddPlayer()
        {
            await Shell.Current.GoToAsync($"{nameof(AddUpdatePlayer)}");
        }

        [RelayCommand]
        async Task Selected(Players player)
        {
            if (player == null)
                return;

            var route = $"{nameof(PlayerDetailPage)}?PlayerID={player.Id}";
            await Shell.Current.GoToAsync(route);
        }

        [RelayCommand]
        async Task Remove(Players player)
        {
            List<Players> removePlayer = new();
            removePlayer = await restService.GetAllBySingleId(player.Id);
            foreach (var item in removePlayer)
            {
                await restService.DeletePlayer(item.EntryId);
            }
            await Refresh();
        }

        [RelayCommand]
        async Task Refresh()
        {
            if (Player.Count > 0)
            {
                Player.Clear();

            }
            Players playerTotals = new();
            //var players = await playerService.GetAllPlayersAsync();
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
                IsBusy = false;

            }
        }
    }
}

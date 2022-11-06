using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
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

        IPlayerService playerService;

        public MainPageViewModel()
        {
            Title = "Player List";

            Player = new ObservableRangeCollection<Players>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddPlayerCommand = new AsyncCommand(AddPlayer);
            RemoveCommand = new AsyncCommand<Players>(Remove);
            SelectedCommand = new AsyncCommand<Players>(Selected);

            playerService = DependencyService.Get<IPlayerService>();
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
            IsBusy = true;
            Player.Clear();
            var players = await playerService.GetAllPlayersAsync();

            Player.AddRange(players);


            IsBusy = false;
        }
    }
}

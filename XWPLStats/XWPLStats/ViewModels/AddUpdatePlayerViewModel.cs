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
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Player),nameof(Player))]
    public class AddUpdatePlayerViewModel : BaseViewModel
    {
        Players Player = new Players();
        string name;
        int gamesWon, gamesLost, id, gamesPlayed;
        decimal average;

        public string PlayerID { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public int GamesWon { get=> gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public int GamesPlayed { get=> gamesPlayed; set => SetProperty(ref gamesPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }

        public AsyncCommand SavePlayerCommand { get; }
        IPlayerService playerService;
        

        public AddUpdatePlayerViewModel()
        {
            Title = "Add/Update Player";
            SavePlayerCommand = new AsyncCommand(SavePlayer);
            playerService = DependencyService.Get<IPlayerService>();
            if(Player !=null)
            {
                var player = playerService.GetSinglePlayer(Player.Id);

            }
        }

        async Task SavePlayer()
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            gamesPlayed = gamesWon + gamesLost;
            decimal avg = Decimal.Round((decimal)(gamesWon / (decimal)gamesPlayed)*100,2);

            Players player = new Players()
            {
                Id = id,
                Name = name,
                GamesWon = gamesWon,
                GamesLost = gamesLost,
                GamesPlayed = gamesPlayed,
                Average = avg,
            };

            await playerService.SavePlayer(player);
            await Shell.Current.GoToAsync("..");
        }

    }
}

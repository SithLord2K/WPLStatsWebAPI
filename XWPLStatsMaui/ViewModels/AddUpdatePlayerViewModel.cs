using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using XWPLStats.Models;
using XWPLStats.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

namespace XWPLStats.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Player),nameof(Player))]
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class AddUpdatePlayerViewModel : BaseViewModel
    {
        Players Player = new Players();
        string name;
        int gamesWon, gamesLost, id, gamesPlayed, weekNumber;
        decimal average;

        public string PlayerID { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public int GamesWon { get=> gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public int GamesPlayed { get=> gamesPlayed; set => SetProperty(ref gamesPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }

        IPlayerService playerService;
        

        public AddUpdatePlayerViewModel()
        {
            Title = "Add/Update Player";
            playerService = new PlayerService();
            if(PlayerID !=null)
            {
                int playerID = int.Parse(PlayerID);
                var player = playerService.GetSinglePlayer(playerID);
            }
        }

        [RelayCommand]
        async Task SavePlayer()
        {
 
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
                WeekNumber = weekNumber
            };

            await playerService.SavePlayer(player);
            await Shell.Current.GoToAsync("..");
        }

    }
}

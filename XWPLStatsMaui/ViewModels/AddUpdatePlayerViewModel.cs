using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using XWPLStats.Models;
using XWPLStats.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace XWPLStats.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Player),nameof(Player))]
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class AddUpdatePlayerViewModel : BaseViewModel
    {
        Players Player = new();
        string name;
        int gamesWon, gamesLost, id, gamesPlayed, weekNumber;
        decimal average;

        public string PlayerID { get; set; }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public int GamesWon { get=> gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public int GamesPlayed { get=> gamesPlayed; set => SetProperty(ref gamesPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }

        readonly IRestService restService;

        public AddUpdatePlayerViewModel()
        {
            Title = "Add/Update Player";
            restService = new RestService();
        }

        [RelayCommand]
        async Task SavePlayer()
        {
 
            gamesPlayed = gamesWon + gamesLost;
            decimal avg = Decimal.Round((decimal)(gamesWon / (decimal)gamesPlayed)*100,2);

            Players player = new()
            {
                Id = id,
                Name = name,
                GamesWon = gamesWon,
                GamesLost = gamesLost,
                GamesPlayed = gamesPlayed,
                Average = avg,
                WeekNumber = weekNumber
            };

            await restService.SavePlayer(player);
            await Shell.Current.GoToAsync("..");
        }

    }
}

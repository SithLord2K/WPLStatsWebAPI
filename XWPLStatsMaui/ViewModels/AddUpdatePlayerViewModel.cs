using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace XWPLStats.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Player), nameof(Player))]
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class AddUpdatePlayerViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public ObservableRangeCollection<Players> Player { get; set; }
        string name;
        private int gamesWon;
        private int gamesLost;
        private int id;
        private int gamesPlayed;
        private int weekNumber;
        decimal average;

        public string PlayerID { get; set; }
        public string Name { get => name; set { name = value; OnPropertyChanged(nameof(Name)); } }
        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public int GamesWon { get => gamesWon; set { gamesWon = value; OnPropertyChanged(nameof(GamesWon)); } }
        public int GamesLost { get => gamesLost; set { gamesLost = value; OnPropertyChanged(nameof(GamesLost)); } }
        public int GamesPlayed { get; set; }
        public decimal Average { get; set; }
        public int WeekNumber { get => weekNumber; set { weekNumber = value; OnPropertyChanged(nameof(WeekNumber)); } }


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
            if (gamesPlayed > 0)
            {
                
                average = Decimal.Round((decimal)(gamesWon / (decimal)gamesPlayed) * 100, 2);

                Players player = new()
                {
                    Id = id,
                    Name = name,
                    GamesWon = gamesWon,
                    GamesLost = gamesLost,
                    GamesPlayed = gamesPlayed,
                    Average = average,
                    WeekNumber = weekNumber
                };

                await restService.SavePlayer(player);
                await Shell.Current.GoToAsync("..");
                return;
            }
            else
            {
                await Shell.Current.DisplayAlert("No Data", "You must fill out the information.", "Ok");
                await Shell.Current.GoToAsync("..");
                return;
            }
    
        }
        

    }
}

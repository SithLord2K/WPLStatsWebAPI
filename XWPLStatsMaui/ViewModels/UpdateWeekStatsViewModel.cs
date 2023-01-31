using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace XWPLStats.ViewModels
{
    public partial class UpdateWeekStatsViewModel : BaseViewModel
    {
        int weeksLost, id, weeksPlayed, weekNumber, teamPlayed;
        string pickedTeam;
        DateTime datePlayed;
        bool weeksWon, forfeit, home;
        decimal average;
        public int Id { get => id; set => SetProperty(ref id, value); }
        public bool WeekWon { get => weeksWon; set => SetProperty(ref weeksWon, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public int WeeksLost { get => weeksLost; set => SetProperty(ref weeksLost, value); }
        public int WeeksPlayed { get => weeksPlayed; set => SetProperty(ref weeksPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }
        public int TeamPlayed { get => teamPlayed; set => SetProperty(ref teamPlayed, value); }
        public string PickedTeam { get => pickedTeam; set => SetProperty(ref pickedTeam, value); }
        public DateTime DatePlayed { get => datePlayed; set => SetProperty(ref datePlayed, value); }
        public bool Forfeit { get => forfeit; set => SetProperty(ref forfeit, value); }
        public bool Home { get => home; set => SetProperty(ref home, value); }

        public ObservableCollection<string> teams = new();
        


        readonly IRestService restService;

        public UpdateWeekStatsViewModel()
        {
            restService = new RestService();

        }
       

        [RelayCommand]
        async Task SaveWeeks()
        {
            List<TeamDetails> teams = new();
            teams = await restService.GetTeamDetails();
            var teamNumber = teams.FirstOrDefault(t => t.TeamName == pickedTeam).TeamNumber;
            
            Weeks week = new()
            {
                WeekNumber = weekNumber,
                WeekWon = weeksWon,
                TeamPlayed = teamNumber,
                DatePlayed = datePlayed.Date, 
                Home = home,
                Forfeit = forfeit,
            };

            await restService.AddWeeks(week);
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        async Task DeleteAllWeeks()
        {
            await restService.RemoveWeeks(id);
        }
    }
}

using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public partial class WeekViewerViewModel : BaseViewModel
    {
        public string teamName;
        public int gamesWon, gamesLost, weekNumber;
        public bool weekWon, home, forfeit;
        public decimal average;
        public List<WeekFullInfo> weekFullInfo;

        public ObservableRangeCollection<WeekFullInfo> WeeksFull { get; set; }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public int GamesWon { get => gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public bool WeekWon { get => weekWon; set => SetProperty(ref weekWon, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }
        public string DatePlayed { get; set; }
        public bool Forfeit { get => forfeit; set => SetProperty(ref forfeit, value); }
        public bool Home { get => home; set => SetProperty(ref home, value); }
        public string TeamName { get => teamName; set => SetProperty(ref teamName, value); }

        readonly IRestService restService;

        public WeekViewerViewModel()
        {
            WeeksFull = new ObservableRangeCollection<WeekFullInfo>();
            restService = new RestService();
        }

        [RelayCommand]
        public async Task Refresh()
        {
            weekFullInfo = new List<WeekFullInfo>();
            List<TeamDetails> whatTeam = new();
            if (WeeksFull.Count != 0)
            {
                WeeksFull.Clear();
            }
            var fullWeeks = await restService.GetAllWeeks(true);
            var playerInfo = await restService.GetAllPlayers();

            foreach (Weeks week in fullWeeks)
            {
                whatTeam = await restService.GetTeamDetails();
                bool testWeek = week.Forfeit;

                WeekFullInfo weekFull = new()
                {

                    GamesWon = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesWon),
                    GamesLost = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesLost),
                    WeekNumber = week.WeekNumber,
                    WeekWon = week.WeekWon,
                    TeamName = whatTeam.Where(td => td.TeamNumber == week.TeamPlayed).FirstOrDefault().TeamName + " - " +
                    whatTeam.Where(td => td.TeamNumber == week.TeamPlayed).FirstOrDefault().Captain,
                    DatePlayed = week.DatePlayed.ToString("MMM. dd yyyy"),
                    Home = week.Home

                };
                if (!testWeek)
                {
                    weekFull.Average = Decimal.Round((decimal)weekFull.GamesWon / ((decimal)weekFull.GamesLost + (decimal)weekFull.GamesWon) * 100, 2);
                }
                else
                {
                    weekFull.Forfeit = week.Forfeit;
                    weekFull.GamesWon = 0;
                    weekFull.GamesLost = 0;
                    weekFull.Average = 0;
                }
                weekFullInfo.Add(weekFull);
            }
            WeeksFull.AddRange(weekFullInfo);
            return;
        }
    }
}

using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public partial class WeekViewerViewModel : BaseViewModel
    {
        public int gamesWon, gamesLost, weekNumber;
        public bool weekWon;
        public decimal average;
        public List<WeekFullInfo> weekFullInfo;
        private List<WeekFullInfo> weekFullInfos;

        public ObservableRangeCollection<WeekFullInfo> WeeksFull { get; set; }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public int GamesWon { get => gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public bool WeekWon { get => weekWon; set => SetProperty(ref weekWon, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }

        readonly IRestService restService;

        public WeekViewerViewModel()
        {
            
            WeeksFull = new ObservableRangeCollection<WeekFullInfo>();
            restService = new RestService();
        }

        [RelayCommand]
        async Task Refresh()
        {
            IsBusy = true;
            weekFullInfo = new List<WeekFullInfo>();
            if(WeeksFull.Count != 0)
            {
                WeeksFull.Clear();
            }
            var fullWeeks = await restService.GetAllWeeks();
            var playerInfo = await restService.GetAllPlayers();
            foreach(Weeks week in fullWeeks)
            {
                WeekFullInfo weekFull = new()
                {
                    GamesWon = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesWon),
                    GamesLost = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesLost),
                    WeekNumber = week.WeekNumber,
                    WeekWon = week.WeekWon
                };
                weekFull.Average = Decimal.Round((decimal)weekFull.GamesWon / ((decimal)weekFull.GamesLost + (decimal)weekFull.GamesWon) * 100, 2);
                weekFullInfo.Add(weekFull);
            }
            WeeksFull.AddRange(weekFullInfo);
            IsBusy = false;
            
        }
    }
}

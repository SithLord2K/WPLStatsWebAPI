using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;
using XWPLStats.Views;

namespace XWPLStats.ViewModels
{
    internal partial class WeekStatsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Weeks> WeekStats { get; set; }
        readonly IRestService restService;
        public WeekStatsViewModel()
        {
            WeekStats = new ObservableRangeCollection<Weeks>();
            restService = new RestService();
        }

        [RelayCommand]
        async Task Refresh()
        {
            IsBusy = true;
            //Get Week Stats
            if (WeekStats.Count != 0)
            {
                WeekStats.Clear();
            }
            var weeks = await restService.GetAllWeeks();
            weeks = weeks.OrderByDescending(a => a.Id);
            Weeks week = new();
            foreach(var w in weeks)
            {
                week.Id = w.Id;
                if(w.WeekWon == true)
                {
                    week.WeekWin += 1;
                }
                else
                {
                    week.WeekLoss += 1;
                }
         
            }
            week.WeeksPlayed = week.WeekWin + week.WeekLoss;
            week.WeeksAverage = Decimal.Round((decimal)(week.WeekWin / (decimal)week.WeeksPlayed) * 100, 2);
            WeekStats.Add(week);
            IsBusy = false;
        }

        [RelayCommand]
        async Task UpdateWeeks()
        {
            var route = $"{nameof(UpdateWeekStats)}";
            await Shell.Current.GoToAsync(route,true);
        }

        [RelayCommand]
        async Task Selected()
        {
            var route = $"{nameof(WeekViewer)}";
            await Shell.Current.GoToAsync(route, true);
        }
    }
}

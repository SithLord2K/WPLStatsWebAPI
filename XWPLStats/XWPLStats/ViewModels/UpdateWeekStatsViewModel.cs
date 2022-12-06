using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    public class UpdateWeekStatsViewModel : BaseViewModel
    {
        int weeksWon, weeksLost, id, weeksPlayed;
        decimal average;
        public int Id { get => id; set => SetProperty(ref id, value); }
        public int WeeksWon { get => weeksWon; set => SetProperty(ref weeksWon, value); }
        public int WeeksLost { get => weeksLost; set => SetProperty(ref weeksLost, value); }
        public int WeeksPlayed { get => weeksPlayed; set => SetProperty(ref weeksPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }

        public AsyncCommand SaveWeeksCommand { get; }
        public AsyncCommand DeleteAllWeeksCommand { get; }
        IWeekService weekService;


        public UpdateWeekStatsViewModel()
        {
            Title = "Add/Update Week Stats";
            SaveWeeksCommand = new AsyncCommand(SaveWeeks);
            DeleteAllWeeksCommand = new AsyncCommand(DeleteAllWeeks);
            weekService = new WeekService();
        }

        async Task SaveWeeks()
        {
            weeksPlayed = weeksWon + weeksLost;
            decimal avg = Decimal.Round((decimal)(weeksWon / (decimal)weeksPlayed) * 100, 2);

            Weeks week = new Weeks()
            {
                Id = Id,
                WeeksWon = weeksWon,
                WeeksLost = weeksLost,
                WeeksPlayed = weeksPlayed,
                WeeksAverage = avg
            };

            await weekService.SaveWeeks(week);
            await Shell.Current.GoToAsync("..");
            
        }

        async Task DeleteAllWeeks()
        {
            await weekService.DeleteAllWeeks();
        }
    }
}

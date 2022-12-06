using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using XWPLStats.Models;
using XWPLStats.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using XWPLStats.Views;

namespace XWPLStats.ViewModels
{
    public partial class UpdateWeekStatsViewModel : BaseViewModel
    {
        int weeksWon, weeksLost, id, weeksPlayed;
        decimal average;
        public int Id { get => id; set => SetProperty(ref id, value); }
        public int WeeksWon { get => weeksWon; set => SetProperty(ref weeksWon, value); }
        public int WeeksLost { get => weeksLost; set => SetProperty(ref weeksLost, value); }
        public int WeeksPlayed { get => weeksPlayed; set => SetProperty(ref weeksPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }

        IWeekService weekService;


        public UpdateWeekStatsViewModel()
        {
            Title = "Add/Update Week Stats";
            weekService = new WeekService();
        }

        [RelayCommand]
        async Task SaveWeeks()
        {
            weeksPlayed = weeksWon + weeksLost;
            decimal avg = decimal.Round((decimal)(weeksWon / (decimal)weeksPlayed) * 100, 2);

            Weeks week = new Weeks()
            {
                Id = Id,
                WeeksWon = weeksWon,
                WeeksLost = weeksLost,
                WeeksPlayed = weeksPlayed,
                WeeksAverage = avg
            };

            await weekService.SaveWeeks(week);
            await Shell.Current.GoToAsync($"{nameof(WeekStatsPage)}");
            
        }

        [RelayCommand]
        async Task DeleteAllWeeks()
        {
            await weekService.DeleteAllWeeks();
        }
    }
}

﻿using MvvmHelpers;
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
        int  weeksLost, id, weeksPlayed, weekNumber;
        bool weeksWon;
        decimal average;
        public int Id { get => id; set => SetProperty(ref id, value); }
        public bool WeekWon { get => weeksWon; set => SetProperty(ref weeksWon, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public int WeeksLost { get => weeksLost; set => SetProperty(ref weeksLost, value); }
        public int WeeksPlayed { get => weeksPlayed; set => SetProperty(ref weeksPlayed, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }

        readonly IRestService restService;

        public UpdateWeekStatsViewModel()
        {
            Title = "Add/Update Week Stats";
            restService = new RestService();
        }

        [RelayCommand]
        async Task SaveWeeks()
        {

            Weeks week = new()
            {
                WeekNumber = weekNumber,
                WeekWon = weeksWon
            };

            await restService.AddWeeks(week);
            await Shell.Current.GoToAsync($"{nameof(WeekStatsPage)}");
            
        }

        [RelayCommand]
        async Task DeleteAllWeeks()
        {
            await restService.RemoveWeeks(id);
        }
    }
}

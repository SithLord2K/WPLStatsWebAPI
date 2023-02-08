﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    [QueryProperty(nameof(Player), nameof(Player))]
    public partial class EditPlayerViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        [ObservableProperty]
        Players player;

        public List<Players> pList = new();
        public string name;
        public int gamesWon, gamesLost, playerId, gamesPlayed, weekNumber;
        public decimal average;
        public string pID;
        public bool _isBusy = false;

        public string PlayerID
        {
            get => pID;
            set => SetProperty(ref pID, value);
        }

        public ObservableRangeCollection<Players> PlayerWeeks { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public int PlayerId { get => playerId; set => SetProperty(ref playerId, value); }
        public int GamesWon { get => gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public int GamesPlayed { get => gamesPlayed; set => SetProperty(ref gamesPlayed, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }
        
        readonly IRestService restService;

        public EditPlayerViewModel()
        {
            PlayerWeeks = new();
            restService = new RestService();
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        [RelayCommand]
        async Task Refresh()
        {
            IsBusy = true;
            if (Player == null)
            {
                return;
            }
            else
            {

                pList = await restService.GetAllBySingleId(Player.Id);
                var sorted = pList.OrderByDescending(a => a.WeekNumber);
                foreach (var item in sorted)
                {
                    item.GamesPlayed = item.GamesWon + item.GamesLost;
                    item.Average = Decimal.Round((decimal)item.GamesWon / (decimal)item.GamesPlayed * 100, 2);
                }
                PlayerWeeks.AddRange(sorted);
                IsBusy = false;
                return;

            }
        }


    }
}

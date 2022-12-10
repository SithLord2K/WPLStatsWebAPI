﻿using System;
using Xamarin.Forms;
using XWPLStats.Services;
using XWPLStats.ViewModels;

namespace XWPLStats.Views
{
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class PlayerDetailPage : ContentPage
    {
        PlayerHelpers playerHelper = new PlayerHelpers();
        PlayerDetailPageViewModel vm = new PlayerDetailPageViewModel();
        IPlayerService playerService;
        public string PlayerID { get; set; }
        public PlayerDetailPage()
        {
            InitializeComponent();
            playerService = new PlayerService();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(PlayerID, out var result);
            var playerdetail = await playerHelper.GetPlayerDetails(result);
            BindingContext = playerdetail;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            string playId = labelId.Text.Replace("ID: ", string.Empty).Trim();
            var route = $"{nameof(AddUpdatePlayer)}?PlayerID={playId}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
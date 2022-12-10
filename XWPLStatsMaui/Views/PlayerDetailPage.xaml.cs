using System;
using XWPLStats.Services;
using XWPLStats.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace XWPLStats.Views
{
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class PlayerDetailPage : ContentPage
    {
        readonly PlayerHelpers playerHelper = new();
        readonly PlayerDetailPageViewModel vm = new();
        public string PlayerID { get; set; }
        public PlayerDetailPage()
        {
            InitializeComponent();
           
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _ = int.TryParse(PlayerID, out var result);
            var playerdetail = await playerHelper.GetPlayerDetails(result);
            BindingContext = playerdetail;
        }

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
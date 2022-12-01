using System;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;
using XWPLStats.ViewModels;

namespace XWPLStats.Views
{

    public partial class PlayerDetailPage : ContentPage
    {
        PlayerDetailPageViewModel vm = new PlayerDetailPageViewModel();
        public PlayerDetailPage()
        {
            InitializeComponent();
            BindingContext= vm;

        }
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    int.TryParse(PlayerID, out var result);
        //    //BindingContext = await playerService.GetSinglePlayer(result);
        //    var vm = new PlayerDetailPageViewModel();
        //    BindingContext = vm;

        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
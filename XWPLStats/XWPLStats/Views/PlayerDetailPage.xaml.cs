using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.Views
{
    [QueryProperty(nameof(PlayerID),nameof(PlayerID))]
    [QueryProperty(nameof(Player), nameof(Player))]
    public partial class PlayerDetailPage : ContentPage
    {
        public string PlayerID { get; set; }
        public Players Player { get; set; }

        readonly IPlayerService playerService;
        public PlayerDetailPage()
        {
            InitializeComponent();
            playerService = new PlayerService();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(PlayerID, out var result);
            BindingContext =await playerService.GetSinglePlayer(result);
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            var route = $"{nameof(AddUpdatePlayer)}?Player={Player}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
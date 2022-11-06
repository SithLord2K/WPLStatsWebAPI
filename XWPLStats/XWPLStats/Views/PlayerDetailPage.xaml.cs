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
        IPlayerService playerService;
        public PlayerDetailPage()
        {
            InitializeComponent();
            playerService = DependencyService.Get<IPlayerService>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(PlayerID, out var result);
            var pID = result;
            BindingContext =await playerService.GetSinglePlayer(result);
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
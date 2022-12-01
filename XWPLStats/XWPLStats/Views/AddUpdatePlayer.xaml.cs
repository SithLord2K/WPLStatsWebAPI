using Xamarin.Forms;
using XWPLStats.Services;

namespace XWPLStats.Views
{
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class AddUpdatePlayer : ContentPage
    {
        public string PlayerID { get; set; }
        readonly IPlayerService playerService;

        public AddUpdatePlayer()
        {
            InitializeComponent();
            playerService = new PlayerService();
        }
        /*protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(PlayerID, out var result);
            var player = await playerService.GetSinglePlayer(result);
            BindingContext = player;
        }*/

    }
}
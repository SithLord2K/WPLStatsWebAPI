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
     }
}
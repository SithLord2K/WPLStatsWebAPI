using XWPLStats.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

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
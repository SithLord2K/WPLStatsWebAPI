using System.ComponentModel;
using Xamarin.Forms;
using XWPLStats.Models;
using XWPLStats.Services;


namespace XWPLStats.ViewModels
{
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class PlayerDetailPageViewModel : ContentPage, INotifyPropertyChanged
    {
        public string playerID;
        public Players Player { get; set; }

        public string PlayerID
        {
            get { return playerID; }
            set
            {
                LoadPlayer(value);
                OnPropertyChanged(nameof(Id));
            }
        }

        public async void LoadPlayer(string value)
        {
            int id = int.Parse(value);
            Player = await playerService.GetSinglePlayer(id);
            
        }

        IPlayerService playerService;
        public PlayerDetailPageViewModel() 
        { 
            playerService= new PlayerService();
        }

    }
}

using System.ComponentModel;
using XWPLStats.Models;
using XWPLStats.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;


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
            Player = await restService.GetSinglePlayer(id);
            
        }

        readonly IRestService restService;
        public PlayerDetailPageViewModel() 
        { 
            restService = new RestService();
        }

    }
}

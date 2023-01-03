using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using XWPLStats.Models;
using XWPLStats.Services;

namespace XWPLStats.ViewModels
{
    [QueryProperty(nameof(PlayerID), nameof(PlayerID))]
    public partial class EditPlayerViewModel : BaseViewModel
    {
        public List<Players> pList = new();
        string name;
        int gamesWon, gamesLost, playerId, gamesPlayed, weekNumber;
        decimal average;
        public string pID;

        public string PlayerID
        {
            get => pID;
            set => SetProperty(ref pID, value);
        }

        public ObservableRangeCollection<Players> Player { get; set; }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public int PlayerId { get => playerId; set => SetProperty(ref playerId, value); }
        public int GamesWon { get => gamesWon; set => SetProperty(ref gamesWon, value); }
        public int GamesLost { get => gamesLost; set => SetProperty(ref gamesLost, value); }
        public int GamesPlayed { get => gamesPlayed; set => SetProperty(ref gamesPlayed, value); }
        public int WeekNumber { get => weekNumber; set => SetProperty(ref weekNumber, value); }
        public decimal Average { get => average; set => SetProperty(ref average, value); }
        IRestService restService;

        public EditPlayerViewModel()
        {
            restService= new RestService();
            bool v = int.TryParse(PlayerID, out int result);
            if(v)
            {
                playerId = result;
            }
        }



        [RelayCommand]
        async Task Refresh()
        {
            //IsBusy = true;
            if (Player.Count != 0)
            {
                Player.Clear();

            }
            var players = await restService.GetAllPlayers();

            if (players.Count == 0)
            {
                return;
            }
            else
            {
                pList = await restService.GetAllBySingleId(playerId);
                var sorted = pList.OrderByDescending(a => a.WeekNumber);
                Player.AddRange(sorted);

            }
            //IsBusy = false;
        }
    }
}

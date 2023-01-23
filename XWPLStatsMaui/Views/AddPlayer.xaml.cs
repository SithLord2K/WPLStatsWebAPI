using XWPLStats.Services;
using XWPLStats.ViewModels;

namespace XWPLStats.Views
{
    public partial class AddPlayer : ContentPage
    {
        readonly IRestService restService;
        List<int> ids = new();
        public AddPlayer()
        {
            InitializeComponent();
            restService = new RestService();
        }

        public int WeekNumber { get; private set; }
        public string Name { get; private set; }

        public async void UpdateWeekNumber(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                bool isValid = int.TryParse(e.NewTextValue, out int result);
                if (isValid && result != 0)
                {
                    var all = await restService.GetDistinctPlayer();
                    foreach(var each in all)
                    {
                            ids.Add(each);
                    }
                    if (ids.Contains(result))
                    {
                        var allPlayer = await restService.GetSinglePlayer(result);
                        var player = allPlayer.OrderBy(a => a.WeekNumber).LastOrDefault();
                        var viewModel = BindingContext as AddUpdatePlayerViewModel;
                        viewModel.Name = player.Name;
                        viewModel.WeekNumber = player.WeekNumber + 1;
                    }
                }
                
            }

        }
    }
}
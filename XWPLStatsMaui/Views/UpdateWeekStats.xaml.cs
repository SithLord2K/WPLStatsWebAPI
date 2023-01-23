using System.Collections.ObjectModel;
using XWPLStats.Services;

namespace XWPLStats.Views
{
    public partial class UpdateWeekStats : ContentPage
    {
        IRestService restService;
        ObservableCollection<string> teams = new ObservableCollection<string>();
        public UpdateWeekStats()
        {
            InitializeComponent();
            restService = new RestService();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var teamList = await restService.GetTeamDetails();
            foreach (var team in teamList)
            {
                teams.Add(team.TeamName);
            }
            TeamPicker.ItemsSource = teams;
            DatePicker.Date = DateTime.Now.Date;
        }
    }
}
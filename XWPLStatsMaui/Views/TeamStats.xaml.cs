using XWPLStats.ViewModels;

namespace XWPLStats.Views
{
    public partial class TeamStats : ContentPage
    {
        public TeamStats(TeamStatsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (TeamStatsViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync(null);

        }
    }
}
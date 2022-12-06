using XWPLStats.ViewModels;

namespace XWPLStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            if (vm.TeamStat.Count == 0)
                await vm.RefreshCommand.ExecuteAsync(null);

            await vm.RefreshCommand.ExecuteAsync(null);
        }
    }
}
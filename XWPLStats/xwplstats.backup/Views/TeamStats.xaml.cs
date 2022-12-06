using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XWPLStats.ViewModels;

namespace XWPLStats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamStats : ContentPage
    {
        public TeamStats()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (TeamStatsViewModel)BindingContext;
            if (vm.TeamStat.Count == 0)
                await vm.RefreshCommand.ExecuteAsync();

            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}
using XWPLStats.ViewModels;

namespace XWPLStats.Views
{
    public partial class WeekStatsPage : ContentPage
    {
        public WeekStatsPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (WeekStatsViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync(null);
        }
    }
}
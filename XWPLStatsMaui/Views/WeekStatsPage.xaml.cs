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
            if (vm.WeekStats.Count == 0)
                await vm.RefreshCommand.ExecuteAsync(null);

            //await vm.RefreshCommand.ExecuteAsync(null);
        }
    }
}
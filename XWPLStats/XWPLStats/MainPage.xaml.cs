using XWPLStats.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace XWPLStats
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (MainPageViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}

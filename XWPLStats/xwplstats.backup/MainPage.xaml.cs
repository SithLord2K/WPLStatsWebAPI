using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XWPLStats.ViewModels;

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

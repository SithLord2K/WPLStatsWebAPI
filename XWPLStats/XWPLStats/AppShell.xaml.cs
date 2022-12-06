using Xamarin.Forms;
using XWPLStats.Views;

namespace XWPLStats
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing
            Routing.RegisterRoute(nameof(AddUpdatePlayer),typeof(AddUpdatePlayer));
            Routing.RegisterRoute(nameof(PlayerDetailPage),typeof(PlayerDetailPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(TeamStats), typeof(TeamStats));
            Routing.RegisterRoute(nameof(UpdateWeekStats), typeof(UpdateWeekStats));
        }
    }
}
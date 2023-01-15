using XWPLStats.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace XWPLStats
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing
            Routing.RegisterRoute(nameof(AddPlayer),typeof(AddPlayer));
            Routing.RegisterRoute(nameof(PlayerDetailPage),typeof(PlayerDetailPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(TeamStats), typeof(TeamStats));
            Routing.RegisterRoute(nameof(WeekStatsPage), typeof(WeekStatsPage));
            Routing.RegisterRoute(nameof(UpdateWeekStats), typeof(UpdateWeekStats));
            Routing.RegisterRoute(nameof(EditPlayer), typeof(EditPlayer));
            Routing.RegisterRoute(nameof(WeekViewer), typeof(WeekViewer));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
        }
    }
}
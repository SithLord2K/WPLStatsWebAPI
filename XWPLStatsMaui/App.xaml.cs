using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MonkeyCache.FileStore;

namespace XWPLStats
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            MainPage = new AppShell();
          
        }

        protected override void OnStart()
        {
   
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

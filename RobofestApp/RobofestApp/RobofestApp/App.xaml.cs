using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RobofestApp.Pages;
using System;
using Xamarin.Forms;
using Plugin.Iconize;
using Xamarin.Forms.Xaml;

namespace RobofestApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=0a8aaa12-ed0f-475f-8771-49e976c7580b;" +
                  "ios=289373ac-99e3-4da2-ba31-720df1844f70;",
                  typeof(Analytics), typeof(Crashes));

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

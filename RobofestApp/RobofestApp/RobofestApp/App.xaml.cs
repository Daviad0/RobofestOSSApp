using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RobofestApp.Pages;
using System;
using Xamarin.Forms;
using Plugin.Iconize;
using Xamarin.Forms.Xaml;
using RobofestApp.Pages.JudgePages;
using Com.OneSignal;
using System.Collections.Generic;
using Com.OneSignal.Abstractions;

namespace RobofestApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new CompetitionManagerPage());

            //Remove this method to stop OneSignal Debugging  
            OneSignal.Current.SetLogLevel(Com.OneSignal.Abstractions.LOG_LEVEL.VERBOSE, Com.OneSignal.Abstractions.LOG_LEVEL.NONE);

            OneSignal.Current.StartInit("1132e860-d6fb-46ef-ba1a-6fd962a8233e")
                .Settings(new Dictionary<string, bool>() {
                    { Com.OneSignal.Abstractions.IOSSettings.kOSSettingsKeyAutoPrompt, false },
                    { Com.OneSignal.Abstractions.IOSSettings.kOSSettingsKeyInAppLaunchURL, false }
                })
            .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)  // <-- !
            .EndInit();

            OneSignal.Current.SendTag("competition", "1");
            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();
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

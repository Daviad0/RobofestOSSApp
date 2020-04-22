using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RobofestApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AppCenter.Analytics;
using RobofestApp.Scripts;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        HubConnection hubConnection;
        private static ObservableCollection<string> data = new ObservableCollection<string>();
        private static TokenStorageModel userToken = new TokenStorageModel();
        public Home()
        {
            
            InitializeComponent();
            /*data.Add("1. 1001-1");
            data.Add("2. 1002-1");
            data.Add("3. 1003-1");
            ThisWontWork.ItemsSource = data;*/
            var getThisToken = new TokenStorageMaster();
            userToken = getThisToken.GetToken();
            BindingContext = new RankViewModel();
            SetUpSignalR();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            BindingContext = new RankViewModel();
        }
        private void TeamDetails(object sender, EventArgs e)
        {
            var button = sender as Button;
            Navigation.PushAsync(new TeamDetails(button.ClassId));
        }
        private void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;
            if(tabbedPage.CurrentPage.ClassId != null)
            {
                if (tabbedPage.CurrentPage.ClassId == "rank")
                {
                    BindingContext = new RankViewModel();
                }
                else if (tabbedPage.CurrentPage.ClassId == "schedule")
                {
                    BindingContext = new ScheduleViewModel();
                }
            }
            
        }

        async private void GoToJudge_Clicked(object sender, EventArgs e)
        {
            await TryTokenLogin();
        }
        async private void SetUpSignalR()
        {
            MasterServerConnection();
            await SignalRConnect();
        }
        private void MasterServerConnection()
        {
            var converter = new ColorTypeConverter();
            var ip = "localhost";
            hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.86.59/scoreHub").Build();

            hubConnection.On("tokenAuthSucc", () =>
            {
                
                Navigation.PushAsync(new FieldSelectionPage());
            });
            hubConnection.On<string, string>("tokenAuthExpired", (correct, wrong) =>
            {
                Navigation.PushAsync(new LoginPage());
            });
            hubConnection.On("tokenAuthInvalid", () =>
            {
                Navigation.PushAsync(new LoginPage());
            });
        }
        async Task SignalRConnect()
        {
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {

            }
        }

        async Task TryTokenLogin()
        {
            try
            {
                await hubConnection.InvokeAsync("tryTokenLogin", userToken.AuthToken, userToken.SessionID);
            }
            catch (Exception ex)
            {
                JudgeError.Text = "Failed";
            }
        }
    }
}
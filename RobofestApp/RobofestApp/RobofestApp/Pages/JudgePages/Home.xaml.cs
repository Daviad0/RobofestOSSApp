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
        static HubConnection hubConnection;
        private static ObservableCollection<string> data = new ObservableCollection<string>();
        private static TokenStorageModel userToken = new TokenStorageModel();
        private static int CompID = (int)Application.Current.Properties["currentCompID"];
        private RankViewModel rankViewModel = new RankViewModel();
        private static bool ConnectionTested;

        public Home()
        {
            ConnectionTested = false;
            InitializeComponent();
            CompID = (int)Application.Current.Properties["currentCompID"];
            /*data.Add("1. 1001-1");
            data.Add("2. 1002-1");
            data.Add("3. 1003-1");
            ThisWontWork.ItemsSource = data;*/
            var getThisToken = new TokenStorageMaster();
            userToken = getThisToken.GetToken();
            SetUpSignalR();
        }
        private void TeamDetails(object sender, EventArgs e)
        {
            var button = sender as Button;
            Navigation.PushAsync(new TeamDetails(button.ClassId));
        }
        private async void CurrentPageHasChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;
            if(tabbedPage.CurrentPage.ClassId != null)
            {
                if (tabbedPage.CurrentPage.ClassId == "rank")
                {
                    await rankViewModel.Update(CompID);
                    BindingContext = rankViewModel;
                    

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
            if (hubConnection == null || hubConnection.State != HubConnectionState.Connected)
            {
                hubConnection = new HubConnectionBuilder().WithUrl($"http://robofest.daviadoprojects.codes/scoreHub").Build();
                hubConnection.ServerTimeout = TimeSpan.FromMinutes(30);
            }

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
            if (hubConnection == null || hubConnection.State != HubConnectionState.Connected)
            {
                Console.WriteLine("Previous Connection Terminated...");
                try
                {
                    await hubConnection.StartAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        async Task ReconnectSignalR()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"http://robofest.daviadoprojects.codes/scoreHub").Build();
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("checkSignalRHub");
        }
        async Task TryTokenLogin()
        {
            try
            {
                await hubConnection.InvokeAsync("tryTokenLogin", userToken.AuthToken, userToken.SessionID);
            }
            catch (Exception ex)
            {

            }
        }

        private async void RankList_Refreshing(object sender, EventArgs e)
        {
            await rankViewModel.Update(CompID);
            BindingContext = rankViewModel;
            RankList.EndRefresh();
        }

        private void ScheduleList_Refreshing(object sender, EventArgs e)
        {
            BindingContext = new ScheduleViewModel();
            ScheduleList.EndRefresh();
        }
    }
}
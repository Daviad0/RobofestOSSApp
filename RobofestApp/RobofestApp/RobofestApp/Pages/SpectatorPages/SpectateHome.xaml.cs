using Microsoft.AspNetCore.SignalR.Client;
using RobofestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages.SpectatorPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class SpectateHome : TabbedPage
    {
        private static int CompID = 0;
        private RankViewModel rankViewModel = new RankViewModel();
        private UpNowViewModel upNowViewModel = new UpNowViewModel();
        static HubConnection hubConnection;
        
        public SpectateHome()
        {
            CompID = 1;
            InitializeComponent();
            SetUpSignalR();
        }
        protected override async void OnAppearing()
        {
            await rankViewModel.Update(CompID);
            BindingContext = rankViewModel;
            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private async void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;
            if (tabbedPage.CurrentPage.ClassId != null)
            {
                if (tabbedPage.CurrentPage.ClassId == "rankings")
                {
                    await rankViewModel.Update(CompID);
                    BindingContext = rankViewModel;
                }
                else if (tabbedPage.CurrentPage.ClassId == "upnow")
                {
                    GetUpNowTeams();
                }
            }
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
                hubConnection = new HubConnectionBuilder().WithUrl($"http://24.35.25.72:80/scoreHub").Build();
                hubConnection.ServerTimeout = TimeSpan.FromMinutes(30);
            }

            hubConnection.On<List<StaticField>>("teamsUpNow", (fields) =>
            {
                SetUpNowTeams(fields);
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
        async Task GetUpNowTeams()
        {
            try
            {
                //CHANGE TO DYNAMIC COMPETITION
                await hubConnection.InvokeAsync("getTeamsUpNow", 1);
            }
            catch(Exception ex)
            {
                await hubConnection.StartAsync();
                await GetUpNowTeams();
            }
        }
        async Task ReconnectSignalR()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"http://24.35.25.72:80/scoreHub").Build();
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("checkSignalRHub");
        }
        private async void SetUpNowTeams(List<StaticField> staticFields)
        {
            await upNowViewModel.Update(staticFields);
            BindingContext = upNowViewModel;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var label = sender as Grid;
            int field = int.Parse(label.ClassId.Split('_')[0]);
            Navigation.PushAsync(new LiveTeam(field));
        }
    }
}
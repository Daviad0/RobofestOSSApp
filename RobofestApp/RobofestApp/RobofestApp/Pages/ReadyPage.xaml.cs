using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AspNetCore.SignalR.Client;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadyPage : ContentPage
    {
        HubConnection hubConnection;
        private static int FieldLoaded;
        public ReadyPage(int Field)
        {
            FieldLoaded = Field;
            InitializeComponent();
            SetUpSignalR();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotReadyPage(FieldLoaded));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage(FieldLoaded));
        }
        async private void SetUpSignalR()
        {
            MasterServerConnection();
            await SignalRConnect();
            await SendFieldStatus();
        }
        private void MasterServerConnection()
        {
            var ip = "localhost";
            hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.86.59/scoreHub").Build();

            hubConnection.On<bool>("changeJudgeLock", (locked) =>
            {
                if(locked == false)
                {
                    Navigation.PushAsync(new MainPage(FieldLoaded));
                }
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
                Error.Text = "Connect";
            }
        }
        async Task SendFieldStatus()
        {
            Error.Text = "Trying";
            try
            {
                await hubConnection.InvokeAsync("initField", FieldLoaded, 2, 0, "1000-1", true, false, "");
            }
            catch (Exception ex)
            {
                Error.Text = "Failed";
            }
        }
    }
}
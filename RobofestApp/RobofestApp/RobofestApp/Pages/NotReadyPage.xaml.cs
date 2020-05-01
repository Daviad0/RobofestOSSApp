using Microsoft.AspNetCore.SignalR.Client;
using RobofestApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotReadyPage : ContentPage
    {
        static HubConnection hubConnection;
        private static int Score;
        private static int FieldLoaded;
        public NotReadyPage(int Field)
        {
            FieldLoaded = Field;
            InitializeComponent();
            SetUpSignalR();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var existingPages = Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Navigation.PushAsync(new ReadyPage(1));

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

            if(hubConnection == null || hubConnection.State != HubConnectionState.Connected)
            {
                hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.86.59/scoreHub").Build();
            }


            hubConnection.On<bool>("changeJudgeLock", (locked) =>
            {
                if (locked == false)
                {
                    try
                    {
                        var existingPages = Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            Navigation.RemovePage(page);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    Navigation.PushAsync(new MainPage(FieldLoaded));
                    //hubConnection.StopAsync();
                }
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
            else
            {
                Console.WriteLine("Already Connected!");
            }
            
        }
        async Task SendFieldStatus()
        {
            
            try
            {
                await hubConnection.InvokeAsync("initField", FieldLoaded, 1, 0, "1000-1", true, false, "");
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
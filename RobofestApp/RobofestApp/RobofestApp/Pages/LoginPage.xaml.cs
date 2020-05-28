using Microsoft.AppCenter.Analytics;
using Microsoft.AspNetCore.SignalR.Client;
using RobofestApp.Scripts;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Iconize;
using RobofestApp.Pages.SpectatorPages;
using RobofestApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private static CompetitionModel[] competitionList = new CompetitionModel[1];
        private CompetitionViewModel compViewModel = new CompetitionViewModel();
        private int compid;
        HubConnection hubConnection;

        public LoginPage()
        {
            InitializeComponent();
            SetUpSignalR();
            

        }
        protected async override void OnAppearing()
        {
            await compViewModel.Update();
            BindingContext = compViewModel;
            competitionList = compViewModel.Comps.ToArray();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            SendScore();
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
            hubConnection = new HubConnectionBuilder().WithUrl($"http://24.35.25.72:80/scoreHub").Build();
            hubConnection.HandshakeTimeout = TimeSpan.FromMinutes(10);
            hubConnection.KeepAliveInterval = TimeSpan.FromSeconds(5);
            hubConnection.Closed += (exception) =>
            {
                if (exception == null)
                {
                    DisplayAlert("OK","Connection closed without error.","OK");
                    Console.WriteLine($"Connection closed due to no error");
                }
                else
                {
                    DisplayAlert("OK", "Connection closed with error.", "OK");
                    Console.WriteLine($"Connection closed due to an error: {exception}");
                }
                return Task.CompletedTask;
            };
            hubConnection.On<string, string>("authSucc", async (token, session) =>
            {
                Analytics.TrackEvent("User " + LoginUsername.Text + " logged in to app. Token: " + token);
                await ProgressLogin.ProgressTo(0.85, 1000, Easing.SinOut);
                var TokenStorageItem = new TokenStorageMaster();
                var returnValue = TokenStorageItem.StoreToken(token, session, 1);
                await ProgressLogin.ProgressTo(0.95, 1000, Easing.SinOut);
                Analytics.TrackEvent(returnValue);
                await ProgressLogin.ProgressTo(1, 1000, Easing.SinOut);
                await Navigation.PushAsync(new Home());
                await hubConnection.StopAsync();
            });
            hubConnection.On("authFail", () =>
            {
                LoginPassword.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                LoginUsername.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
            });
            hubConnection.On("authRoleFail", () =>
            {
                LoginPassword.TextColor = (Color)converter.ConvertFromInvariantString("Color.Yellow");
                LoginUsername.TextColor = (Color)converter.ConvertFromInvariantString("Color.Yellow");
            });
            hubConnection.On("authAccept", () =>
            {
                ProgressLogin.ProgressTo(0.1, 1000, Easing.SinOut);
                LoginPending.IsVisible = false;
                LoginAccepted.IsVisible = true;
            });
            hubConnection.On<int>("authProgress", (progress) =>
            {
                ProgressLogin.ProgressTo(progress / 100, 1000, Easing.SinOut);
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

        async Task SendScore()
        {
            try
            {
                await hubConnection.InvokeAsync("appLogin", LoginUsername.Text, LoginPassword.Text, compid);
            }
            catch (Exception ex)
            {
                await hubConnection.StartAsync();
                await SendScore();
            }
        }

        private void Form_TextChanged(object sender, TextChangedEventArgs e)
        {
            var converter = new ColorTypeConverter();
            LoginPassword.TextColor = (Color)converter.ConvertFromInvariantString("Color.Black");
            LoginUsername.TextColor = (Color)converter.ConvertFromInvariantString("Color.Black");
        }

        private void autoLogin_Clicked(object sender, EventArgs e)
        {
            LoginUsername.Text = "testemail1@gmail.com";
            LoginPassword.Text = "Robofestwte@1";
            SendScore();
        }

        private void spectate_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpectateHome());
        }

        private async void locationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            autoLogin.IsEnabled = true;
            Application.Current.Properties["currentCompID"] = competitionList[locationPicker.SelectedIndex].CompID;
            compid = competitionList[locationPicker.SelectedIndex].CompID;
        }
    }
}
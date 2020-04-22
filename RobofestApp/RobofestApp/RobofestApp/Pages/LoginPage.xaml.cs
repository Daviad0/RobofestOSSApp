﻿using Microsoft.AppCenter.Analytics;
using Microsoft.AspNetCore.SignalR.Client;
using RobofestApp.Scripts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        HubConnection hubConnection;
        public LoginPage()
        {
            InitializeComponent();
            SetUpSignalR();
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
            hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.86.59/scoreHub").Build();

            hubConnection.On<string, string>("authSucc", async (token, session) =>
            {
                Analytics.TrackEvent("User " + LoginUsername.Text + " logged in to app. Token: " + token);
                await ProgressLogin.ProgressTo(0.85, 1000, Easing.Linear);
                var TokenStorageItem = new TokenStorageMaster();
                var returnValue = TokenStorageItem.StoreToken(token, session);
                await ProgressLogin.ProgressTo(0.95, 1000, Easing.Linear);
                Analytics.TrackEvent(returnValue);
                await ProgressLogin.ProgressTo(1, 1000, Easing.Linear);
                await Navigation.PushAsync(new Home());
            });
            hubConnection.On("authFail", () =>
            {
                LoginPassword.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                LoginUsername.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
            });
            hubConnection.On("authAccept", () =>
            {
                ProgressLogin.ProgressTo(0.1, 1000, Easing.Linear);
                LoginPending.IsVisible = false;
                LoginAccepted.IsVisible = true;
            });
            hubConnection.On<int>("authProgress", (progress) =>
            {
                ProgressLogin.ProgressTo(progress / 100, 1000, Easing.Linear);
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
                await hubConnection.InvokeAsync("appLogin", LoginUsername.Text, LoginPassword.Text);
            }
            catch (Exception ex)
            {
               
            }
        }

        private void Form_TextChanged(object sender, TextChangedEventArgs e)
        {
            var converter = new ColorTypeConverter();
            LoginPassword.TextColor = (Color)converter.ConvertFromInvariantString("Color.Black");
            LoginUsername.TextColor = (Color)converter.ConvertFromInvariantString("Color.Black");
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.AspNetCore.SignalR.Client;
using RobofestApp.Pages;

namespace RobofestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        static HubConnection hubConnection;
        private static int[] BottleScores = { 0, 0, 0, 0, 0 };
        private static int[] GeneralPoints = { 0, 0, 0 };
        private static int[] BallPoints = { 0, 0, 0, 0 };
        private static string Data = "";
        private static int TotalScore = 0;
        private static int CurrentField;
        private static bool ReviewingScores = false;
        private static bool ConnectionTested = false;
        public MainPage(int Field)
        {
            ReviewingScores = true;
            ConnectionTested = false;
            CurrentField = Field;
            SetUpSignalR();
            InitializeComponent();
            Array.Clear(BottleScores, 0, BottleScores.Length);
            Array.Clear(GeneralPoints, 0, GeneralPoints.Length);
            Array.Clear(BallPoints, 0, BallPoints.Length);
            TotalScore = 0;
            Data = "";
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void bot1_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if(sender == bot1_opt1)
            {
                BottleScores[0] = 0;
                bot1_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if(sender == bot1_opt2)
            {
                BottleScores[0] = 3;
                bot1_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot1_opt3)
            {
                BottleScores[0] = 10;
                bot1_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot1_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot1_opt3.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentBottleScore.Text = "Bottle Score: " + (BottleScores[0] + BottleScores[1] + BottleScores[2] + BottleScores[3] + BottleScores[4]).ToString();
            await CalculateScoresAsync();
        }
        private async void bot2_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == bot2_opt1)
            {
                BottleScores[1] = 0;
                bot2_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot2_opt2)
            {
                BottleScores[1] = 4;
                bot2_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot2_opt3)
            {
                BottleScores[1] = 11;
                bot2_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot2_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot2_opt3.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentBottleScore.Text = "Bottle Score: " + (BottleScores[0] + BottleScores[1] + BottleScores[2] + BottleScores[3] + BottleScores[4]).ToString();
            await CalculateScoresAsync();
        }
        private async void bot3_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == bot3_opt1)
            {
                BottleScores[2] = 0;
                bot3_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot3_opt2)
            {
                BottleScores[2] = 4;
                bot3_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot3_opt3)
            {
                BottleScores[2] = 11;
                bot3_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot3_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot3_opt3.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentBottleScore.Text = "Bottle Score: " + (BottleScores[0] + BottleScores[1] + BottleScores[2] + BottleScores[3] + BottleScores[4]).ToString();
            await CalculateScoresAsync();
        }
        private async void bot4_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == bot4_opt1)
            {
                BottleScores[3] = 0;
                bot4_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot4_opt2)
            {
                BottleScores[3] = 3;
                bot4_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == bot4_opt3)
            {
                BottleScores[3] = 10;
                bot4_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot4_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                bot4_opt3.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentBottleScore.Text = "Bottle Score: " + (BottleScores[0] + BottleScores[1] + BottleScores[2] + BottleScores[3] + BottleScores[4]).ToString();
            await CalculateScoresAsync();
        }
        private async void bot5_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == bot5_opt1)
            {
                BottleScores[4] = 0;
                bot5_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
            }
            else if (sender == bot5_opt2)
            {
                BottleScores[4] = -3;
                bot5_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt3.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
            }
            else if (sender == bot5_opt3)
            {
                BottleScores[4] = -3;
                bot5_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                bot5_opt3.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                bot5_opt3.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentBottleScore.Text = "Bottle Score: " + (BottleScores[0] + BottleScores[1] + BottleScores[2] + BottleScores[3] + BottleScores[4]).ToString();
            await CalculateScoresAsync();
        }

        private async void end_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == end_opt1)
            {
                GeneralPoints[0] = 0;
                end_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                end_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                end_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                end_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if(sender == end_opt2)
            {
                GeneralPoints[0] = 12;
                end_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                end_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                end_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                end_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentGeneralScore.Text = "General Score: " + (GeneralPoints[0] + GeneralPoints[1] + GeneralPoints[2]).ToString();
            await CalculateScoresAsync();
        }

        private async void rin_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == rin_opt1)
            {
                GeneralPoints[1] = 0;
                rin_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                rin_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                rin_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                rin_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == rin_opt2)
            {
                GeneralPoints[1] = 12;
                rin_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                rin_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                rin_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                rin_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentGeneralScore.Text = "General Score: " + (GeneralPoints[0] + GeneralPoints[1] + GeneralPoints[2]).ToString();
            await CalculateScoresAsync();
        }
        private async void fre_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == fre_opt1)
            {
                GeneralPoints[2] = 0;
                fre_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                fre_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                fre_opt1.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                fre_opt2.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
            }
            else if (sender == fre_opt2)
            {
                GeneralPoints[2] = -3;
                fre_opt1.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                fre_opt2.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                fre_opt1.TextColor = (Color)converter.ConvertFromInvariantString("Color.Red");
                fre_opt2.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
            }
            currentGeneralScore.Text = "General Score: " + (GeneralPoints[0] + GeneralPoints[1] + GeneralPoints[2]).ToString();
            await CalculateScoresAsync();
        }
        private async void whiteBalls(object sender, EventArgs e)
        {
            if(sender == whiteBallsUp)
            {
                whiteBallsUp.IsEnabled = true;
                if(BallPoints[0] < 2)
                {
                    BallPoints[0] += 1;
                }
                else if(BallPoints[0] == 2)
                {
                    BallPoints[0] += 1;
                    whiteBallsUp.IsEnabled = false;
                }
            }
            else
            {
                whiteBallsUp.IsEnabled = true;
                if (BallPoints[0] > 1)
                {
                    BallPoints[0] -= 1;
                }
                else if (BallPoints[0] == 1)
                {
                    BallPoints[0] -= 1;
                    whiteBallsDown.IsEnabled = false;
                }
            }
            whiteBallsLabel.Text = BallPoints[0].ToString();
            currentBallScore.Text = "Ball Score: " + ((BallPoints[0] * 15) + (BallPoints[1] * 18) + (BallPoints[2] * -3) + BallPoints[3]).ToString();
            await CalculateScoresAsync();
        }
        private async void orangeBalls(object sender, EventArgs e)
        {
            if (sender == orangeBallsUp)
            {
                orangeBallsDown.IsEnabled = true;
                if (BallPoints[1] < 1)
                {
                    BallPoints[1] += 1;
                }
                else if (BallPoints[1] == 1)
                {
                    BallPoints[1] += 1;
                    orangeBallsUp.IsEnabled = false;
                }
            }
            else
            {
                orangeBallsUp.IsEnabled = true;
                if (BallPoints[1] > 1)
                {
                    BallPoints[1] -= 1;
                }
                else if (BallPoints[1] == 1)
                {
                    BallPoints[1] -= 1;
                    orangeBallsDown.IsEnabled = false;
                }
            }
            orangeBallsLabel.Text = BallPoints[1].ToString();
            currentBallScore.Text = "Ball Score: " + ((BallPoints[0] * 15) + (BallPoints[1] * 18) + (BallPoints[2] * -3) + BallPoints[3]).ToString();
            await CalculateScoresAsync();
        }
        private async void invalidBalls(object sender, EventArgs e)
        {
            if (sender == invalidBallsUp)
            {
                invalidBallsDown.IsEnabled = true;
                if (BallPoints[2] < 4)
                {
                    BallPoints[2] += 1;
                }
                else if (BallPoints[2] == 4)
                {
                    BallPoints[2] += 1;
                    invalidBallsUp.IsEnabled = false;
                }
            }
            else
            {
                invalidBallsUp.IsEnabled = true;
                if (BallPoints[2] > 1)
                {
                    BallPoints[2] -= 1;
                }
                else if (BallPoints[2] == 1)
                {
                    BallPoints[2] -= 1;
                    invalidBallsDown.IsEnabled = false;
                }
            }
            invalidBallsLabel.Text = BallPoints[2].ToString();
            currentBallScore.Text = "Ball Score: " + ((BallPoints[0] * 15) + (BallPoints[1] * 18) + (BallPoints[2] * -3) + BallPoints[3]).ToString();
            await CalculateScoresAsync();
        }
        private async void offBalls(object sender, EventArgs e)
        {
            if (sender == offBallsUp)
            {
                offBallsDown.IsEnabled = true;
                if (BallPoints[3] < 4)
                {
                    BallPoints[3] += 1;
                }
                else if (BallPoints[3] == 4)
                {
                    BallPoints[3] += 1;
                    offBallsUp.IsEnabled = false;
                }
            }
            else
            {
                offBallsUp.IsEnabled = true;
                if (BallPoints[3] > 1)
                {
                    BallPoints[3] -= 1;
                }
                else if (BallPoints[3] == 1)
                {
                    BallPoints[3] -= 1;
                    offBallsDown.IsEnabled = false;
                }
            }
            offBallsLabel.Text = BallPoints[3].ToString();
            currentBallScore.Text = "Ball Score: " + ((BallPoints[0] * 15) + (BallPoints[1] * 18) + (BallPoints[2] * -3) + BallPoints[3]).ToString();
            await CalculateScoresAsync();
        }
        private async Task CalculateScoresAsync()
        {
            Data = BottleScores[0].ToString() + "/" + BottleScores[1].ToString() + "/" + BottleScores[2].ToString() + "/" + BottleScores[3].ToString() + "/" + BottleScores[4].ToString() + "^" + (BallPoints[0]*15).ToString() + "/" + (BallPoints[1]*18).ToString() + "/" + (BallPoints[2]*-3).ToString() + "/" + BallPoints[3].ToString() + "^" + GeneralPoints[0].ToString() + "/" + GeneralPoints[2].ToString() + "^" + GeneralPoints[2].ToString();
            TotalScore = BottleScores[0] + BottleScores[1] + BottleScores[2] + BottleScores[3] + BottleScores[4] + (BallPoints[0] * 15) + (BallPoints[1] * 18) + (BallPoints[2] * -3) + BallPoints[3] + GeneralPoints[0] + GeneralPoints[1] + GeneralPoints[2];
            TotalScoreLabel.Text = "Total Score: "+TotalScore.ToString();
            await SendScore();
            
        }
        async private void SetUpSignalR()
        {
            MasterServerConnection();
            await SignalRConnect();
        }
        private void MasterServerConnection()
        {
            var ip = "localhost";
            if (hubConnection == null || hubConnection.State != HubConnectionState.Connected)
            {
                hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.86.59/scoreHub").Build();
            }

            hubConnection.On<int, int, string, int>("changeGlobalTimer", (minutes, seconds, message, status) =>
            {
                string secondsview = seconds.ToString();
                if(secondsview.Length == 1)
                {
                    secondsview = "0" + secondsview;
                }
                Device.BeginInvokeOnMainThread(() => {
                    Title = "Field " + CurrentField.ToString() + ": Scoring (" + minutes.ToString() + ":" + secondsview.ToString() + ")";
                });
                
            });
            hubConnection.On("signalRConnected", () =>
            {
                ConnectionTested = true;
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
                Console.WriteLine("Testing Connection");
                await hubConnection.InvokeAsync("checkSignalRHub");
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    Console.WriteLine("Checking Connection...");
                    if(ConnectionTested == true)
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Did not recieve back, reconnecting...");
                        ReconnectSignalR();
                        return true;
                    }
                    
                });
            }
        }
        async Task ReconnectSignalR()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"http://192.168.86.59/scoreHub").Build();
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("checkSignalRHub");
        }
        async Task SendScore()
        {
            try
            {
                await hubConnection.InvokeAsync("initField", CurrentField, 5, TotalScore, "1001-1", true, false, Data);
            }
            catch(Exception ex)
            {

            }
        }

        private void SubmitScores_Clicked(object sender, EventArgs e)
        {
            if (ReviewingScores == false)
            { 
                var converter = new ColorTypeConverter();
                SubmitScores.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                ReviewingScores = true;
                fre_opt1.IsEnabled = false;
                fre_opt2.IsEnabled = false;
                rin_opt1.IsEnabled = false;
                rin_opt2.IsEnabled = false;
                end_opt1.IsEnabled = false;
                end_opt2.IsEnabled = false;
                bot5_opt1.IsEnabled = false;
                bot5_opt2.IsEnabled = false;
                bot5_opt3.IsEnabled = false;
                bot4_opt1.IsEnabled = false;
                bot4_opt2.IsEnabled = false;
                bot4_opt3.IsEnabled = false;
                bot3_opt1.IsEnabled = false;
                bot3_opt2.IsEnabled = false;
                bot3_opt3.IsEnabled = false;
                bot2_opt1.IsEnabled = false;
                bot2_opt2.IsEnabled = false;
                bot2_opt3.IsEnabled = false;
                bot1_opt1.IsEnabled = false;
                bot1_opt2.IsEnabled = false;
                bot1_opt3.IsEnabled = false;
                whiteBallsUp.IsEnabled = false;
                whiteBallsDown.IsEnabled = false;
                orangeBallsUp.IsEnabled = false;
                orangeBallsDown.IsEnabled = false;
                invalidBallsUp.IsEnabled = false;
                invalidBallsDown.IsEnabled = false;
                offBallsUp.IsEnabled = false;
                offBallsDown.IsEnabled = false;
                scrollViewForm.ScrollToAsync(0, 0, true);
                Title = "Field 1: Reviewing";
                SubmitScores.BackgroundColor = (Color)converter.ConvertFromInvariantString("#15d656");
                //SubmitScores.IsEnabled = false;
                SubmitScores.Text = "Submit to Database";
                EditScores.IsVisible = true;
            }
            else
            {
                Navigation.PushAsync(new Home());
                //hubConnection.StopAsync();
            }

        }

        private void EditScores_Clicked(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            ReviewingScores = false;
            fre_opt1.IsEnabled = true;
            fre_opt2.IsEnabled = true;
            rin_opt1.IsEnabled = true;
            rin_opt2.IsEnabled = true;
            end_opt1.IsEnabled = true;
            end_opt2.IsEnabled = true;
            bot5_opt1.IsEnabled = true;
            bot5_opt2.IsEnabled = true;
            bot5_opt3.IsEnabled = true;
            bot4_opt1.IsEnabled = true;
            bot4_opt2.IsEnabled = true;
            bot4_opt3.IsEnabled = true;
            bot3_opt1.IsEnabled = true;
            bot3_opt2.IsEnabled = true;
            bot3_opt3.IsEnabled = true;
            bot2_opt1.IsEnabled = true;
            bot2_opt2.IsEnabled = true;
            bot2_opt3.IsEnabled = true;
            bot1_opt1.IsEnabled = true;
            bot1_opt2.IsEnabled = true;
            bot1_opt3.IsEnabled = true;
            whiteBallsUp.IsEnabled = true;
            whiteBallsDown.IsEnabled = true;
            orangeBallsUp.IsEnabled = true;
            orangeBallsDown.IsEnabled = true;
            invalidBallsUp.IsEnabled = true;
            invalidBallsDown.IsEnabled = true;
            offBallsUp.IsEnabled = true;
            offBallsDown.IsEnabled = true;
            scrollViewForm.ScrollToAsync(0, 0, true);
            Title = "Field " + CurrentField.ToString() + ": Scoring (0:00)";
            SubmitScores.Text = "Review Scores";
            SubmitScores.TextColor = (Color)converter.ConvertFromInvariantString("#15d656");
            SubmitScores.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Transparent");
            EditScores.IsVisible = false;
            //SubmitScores.IsEnabled = true;
        }
    }
}

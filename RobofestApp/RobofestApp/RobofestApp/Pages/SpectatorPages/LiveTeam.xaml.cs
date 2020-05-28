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
    public partial class LiveTeam : ContentPage
    {
        static HubConnection hubConnection;
        private int CurrentField = 0;
        public LiveTeam(int passedField)
        {
            InitializeComponent();
            CurrentField = passedField;
            SetUpSignalR();
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

            hubConnection.On<int, int, int, string, bool, bool, string, int>("appLiveScores", (field, status, score, teamnumber, connection, matchkeeper, data, compid) =>
            {
                if(compid == 1)
                {
                    if(field == CurrentField)
                    {
                        //START LIVE TEAM
                        if(status == 0)
                        {
                            fieldStatus.Text = "Awaiting Match Start...";
                            if (!matchkeeper)
                            {
                                Title = "Field " + CurrentField.ToString() + " Up Now: " + teamnumber;
                            }
                        }
                        else if(status == 1)
                        {
                            fieldStatus.Text = "Field Judge Logged In...";
                            if (!matchkeeper)
                            {
                                Title = "Field " + CurrentField.ToString() + " Up Now: " + teamnumber;
                            }
                        }
                        else if (status == 2)
                        {
                            fieldStatus.Text = "Field Judge Ready...";
                            if (!matchkeeper)
                            {
                                Title = "Field " + CurrentField.ToString() + " Up Now: " + teamnumber;
                            }
                        }
                        else if (status == 3)
                        {
                            fieldStatus.Text = "Field Scored!";
                            if (!matchkeeper)
                            {
                                Title = "Field " + CurrentField.ToString() + " Up Now: " + teamnumber;
                            }
                        }
                        else if (status == 5)
                        {
                            var datasplits = data.Split('^');
                            var bottles = datasplits[0].Split('/');
                            var balls = datasplits[1].Split('/');
                            var general = datasplits[2].Split('/');
                            var penalty = datasplits[3].Split('/');
                            totalScore.Text = "Total Score: " + score.ToString();
                            if(int.Parse(bottles[0]) > 0)
                            {
                                bot1img.Source = ImageSource.FromFile("bottlepos.png");
                            }
                            else
                            {
                                bot1img.Source = ImageSource.FromFile("bottlereg.png");
                            }
                            bot1score.Text = bottles[0];
                            if (int.Parse(bottles[1]) > 0)
                            {
                                bot2img.Source = ImageSource.FromFile("bottlepos.png");
                            }
                            else
                            {
                                bot2img.Source = ImageSource.FromFile("bottlereg.png");
                            }
                            bot2score.Text = bottles[1];
                            if (int.Parse(bottles[2]) > 0)
                            {
                                bot3img.Source = ImageSource.FromFile("bottlepos.png");
                            }
                            else
                            {
                                bot3img.Source = ImageSource.FromFile("bottlereg.png");
                            }
                            bot3score.Text = bottles[2];
                            if (int.Parse(bottles[3]) > 0)
                            {
                                bot4img.Source = ImageSource.FromFile("bottlepos.png");
                            }
                            else
                            {
                                bot4img.Source = ImageSource.FromFile("bottlereg.png");
                            }
                            bot4score.Text = bottles[3];
                            if (int.Parse(bottles[4]) < 0)
                            {
                                bot5img.Source = ImageSource.FromFile("bottleneg.png");
                            }
                            else
                            {
                                bot5img.Source = ImageSource.FromFile("bottlereg.png");
                            }
                            bot5score.Text = bottles[4];
                            if ((int.Parse(balls[0]) / 15) > 0)
                            {
                                whiteb1.Source = ImageSource.FromFile("whiteyes.png");
                                if((int.Parse(balls[0]) / 15) > 1)
                                {
                                    whiteb2.Source = ImageSource.FromFile("whiteyes.png");
                                    if ((int.Parse(balls[0]) / 15) > 2)
                                    {
                                        whiteb3.Source = ImageSource.FromFile("whiteyes.png");
                                    }
                                    else
                                    {
                                        whiteb3.Source = ImageSource.FromFile("whiteno.png");
                                    }
                                }
                                else
                                {
                                    whiteb2.Source = ImageSource.FromFile("whiteno.png");
                                    whiteb3.Source = ImageSource.FromFile("whiteno.png");
                                }
                            }
                            else
                            {
                                whiteb1.Source = ImageSource.FromFile("whiteno.png");
                                whiteb2.Source = ImageSource.FromFile("whiteno.png");
                                whiteb3.Source = ImageSource.FromFile("whiteno.png");
                            }
                            numWhite.Text = (int.Parse(balls[0]) / 15).ToString();
                            if ((int.Parse(balls[1]) / 18) > 0)
                            {
                                orangeb1.Source = ImageSource.FromFile("orangeyes.png");
                                if ((int.Parse(balls[1]) / 18) > 1)
                                {
                                    orangeb2.Source = ImageSource.FromFile("orangeyes.png");
                                }
                                else
                                {
                                    orangeb2.Source = ImageSource.FromFile("orangeno.png");
                                }
                            }
                            else
                            {
                                orangeb1.Source = ImageSource.FromFile("orangeno.png");
                                orangeb2.Source = ImageSource.FromFile("orangeno.png");
                            }
                            numOrange.Text = (int.Parse(balls[1]) / 18).ToString();
                            if ((int.Parse(balls[2]) / -3) > 0)
                            {
                                invalidb1.Source = ImageSource.FromFile("invalidyes.png");
                                if ((int.Parse(balls[2]) / -3) > 1)
                                {
                                    invalidb2.Source = ImageSource.FromFile("invalidyes.png");
                                    if ((int.Parse(balls[2]) / -3) > 2)
                                    {
                                        invalidb3.Source = ImageSource.FromFile("invalidyes.png");
                                        if ((int.Parse(balls[2]) / -3) > 3)
                                        {
                                            invalidb4.Source = ImageSource.FromFile("invalidyes.png");
                                            if ((int.Parse(balls[2]) / -3) > 4)
                                            {
                                                invalidb5.Source = ImageSource.FromFile("invalidyes.png");
                                            }
                                            else
                                            {
                                                invalidb5.Source = ImageSource.FromFile("invalidno.png");
                                            }
                                        }
                                        else
                                        {
                                            invalidb4.Source = ImageSource.FromFile("invalidno.png");
                                            invalidb5.Source = ImageSource.FromFile("invalidno.png");
                                        }
                                    }
                                    else
                                    {
                                        invalidb3.Source = ImageSource.FromFile("invalidno.png");
                                        invalidb4.Source = ImageSource.FromFile("invalidno.png");
                                        invalidb5.Source = ImageSource.FromFile("invalidno.png");
                                    }
                                }
                                else
                                {
                                    invalidb2.Source = ImageSource.FromFile("invalidno.png");
                                    invalidb3.Source = ImageSource.FromFile("invalidno.png");
                                    invalidb4.Source = ImageSource.FromFile("invalidno.png");
                                    invalidb5.Source = ImageSource.FromFile("invalidno.png");
                                }
                            }
                            else
                            {
                                invalidb1.Source = ImageSource.FromFile("invalidno.png");
                                invalidb2.Source = ImageSource.FromFile("invalidno.png");
                                invalidb3.Source = ImageSource.FromFile("invalidno.png");
                                invalidb4.Source = ImageSource.FromFile("invalidno.png");
                                invalidb5.Source = ImageSource.FromFile("invalidno.png");
                            }
                            if(int.Parse(general[0]) > 0)
                            {
                                endgame.BackgroundColor = (Color)converter.ConvertFromInvariantString("#15d656");
                                endgame.BorderColor = (Color)converter.ConvertFromInvariantString("#15d656");
                            }
                            else
                            {
                                endgame.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Gray");
                                endgame.BorderColor = (Color)converter.ConvertFromInvariantString("Color.Gray");
                            }
                            if (int.Parse(general[1]) > 0)
                            {
                                robotIntact.BackgroundColor = (Color)converter.ConvertFromInvariantString("#15d656");
                                robotIntact.BorderColor = (Color)converter.ConvertFromInvariantString("#15d656");
                            }
                            else
                            {
                                robotIntact.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.Gray");
                                robotIntact.BorderColor = (Color)converter.ConvertFromInvariantString("Color.Gray");
                            }
                        }
                    }
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
        }
        async Task ReconnectSignalR()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"http://24.35.25.72:80/scoreHub").Build();
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("checkSignalRHub");
        }
    }
}
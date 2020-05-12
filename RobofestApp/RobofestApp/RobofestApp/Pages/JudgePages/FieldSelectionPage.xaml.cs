using Microsoft.AspNetCore.SignalR.Client;
using RobofestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldSelectionPage : ContentPage
    {
        static HubConnection hubConnection;
        private static int FieldNum = 0;
        private static int Round = 0;
        private static bool Rerun = false;
        private static bool Usable = true;
        private static int[] TeamIDs = new int[6];
        private static string[] TeamNumbers = new string[6];
        private static int CompID = (int)Application.Current.Properties["currentCompID"];
        private static int[] Rounds = new int[6];
        private static bool[] Reruns = new bool[6];
        private static bool[] Tests = new bool[6];
        private static bool[] Validates = new bool[6];
        private static int SelectedIndex = 0;
        public FieldSelectionPage()
        {
            InitializeComponent();
            SetUpSignalR();
        }
        private void NextPage(object sender, EventArgs e)
        {
            var pushteamdata = new TeamDataStorage();
            pushteamdata.Field = FieldNum;
            pushteamdata.Rerun = Rerun;
            pushteamdata.TeamID = TeamIDs[SelectedIndex];
            pushteamdata.TeamNumber = TeamNumbers[SelectedIndex];
            pushteamdata.Test = Tests[SelectedIndex];
            pushteamdata.Valid = Validates[SelectedIndex];
            pushteamdata.Round = Round;
            Navigation.PushAsync(new CheckSelectionPage(pushteamdata));
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
           
        }
        private void ChangeField(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (Validates[0])
            {
                f1select.Style = (Style)Resources["judgePositive"] as Style;
            }
            else
            {
                f1select.Style = (Style)Resources["judgeInvalid"] as Style;
            }
            if (Validates[1])
            {
                f2select.Style = (Style)Resources["judgePositive"] as Style;
            }
            else
            {
                f2select.Style = (Style)Resources["judgeInvalid"] as Style;
            }
            if (Validates[2])
            {
                f3select.Style = (Style)Resources["judgePositive"] as Style;
            }
            else
            {
                f3select.Style = (Style)Resources["judgeInvalid"] as Style;
            }
            if (Validates[3])
            {
                f4select.Style = (Style)Resources["judgePositive"] as Style;
            }
            else
            {
                f4select.Style = (Style)Resources["judgeInvalid"] as Style;
            }
            if (Validates[4])
            {
                f5select.Style = (Style)Resources["judgePositive"] as Style;

            }
            else
            {
                f5select.Style = (Style)Resources["judgeInvalid"] as Style;
            }
            if (Validates[5])
            {
                f6select.Style = (Style)Resources["judgePositive"] as Style;
            }
            else
            {
                f6select.Style = (Style)Resources["judgeInvalid"] as Style;
            }
            if (sender == f1select)
            {
                FieldNum = 1;
                SelectedIndex = 0;
                if (Validates[0])
                {
                    f1select.Style = (Style)Resources["judgePositiveSelected"] as Style;
                }
                else
                {
                    f1select.Style = (Style)Resources["judgeInvalidSelected"] as Style;
                }
            }
            else if(sender == f2select)
            {
                FieldNum = 2;
                SelectedIndex = 1;
                if (Validates[1])
                {
                    f2select.Style = (Style)Resources["judgePositiveSelected"] as Style;
                }
                else
                {
                    f2select.Style = (Style)Resources["judgeInvalidSelected"] as Style;
                }
            }
            else if(sender == f3select)
            {
                FieldNum = 3;
                SelectedIndex = 2;
                if (Validates[2])
                {
                    f3select.Style = (Style)Resources["judgePositiveSelected"] as Style;
                }
                else
                {
                    f3select.Style = (Style)Resources["judgeInvalidSelected"] as Style;
                }
            }
            else if (sender == f4select)
            {
                FieldNum = 4;
                SelectedIndex = 3;
                if (Validates[3])
                {
                    f4select.Style = (Style)Resources["judgePositiveSelected"] as Style;
                }
                else
                {
                    f4select.Style = (Style)Resources["judgeInvalidSelected"] as Style;
                }
            }
            else if (sender == f5select)
            {
                FieldNum = 5;
                SelectedIndex = 4;
                if (Validates[4])
                {
                    f5select.Style = (Style)Resources["judgePositiveSelected"] as Style;
                }
                else
                {
                    f5select.Style = (Style)Resources["judgeInvalidSelected"] as Style;
                }
            }
            else if (sender == f6select)
            {
                FieldNum = 6;
                SelectedIndex = 5;
                if (Validates[5])
                {
                    f6select.Style = (Style)Resources["judgePositiveSelected"] as Style;
                }
                else
                {
                    f6select.Style = (Style)Resources["judgeInvalidSelected"] as Style;
                }
            }
            if (!Validates[SelectedIndex])
            {
                WarningNotValid.IsVisible = true;
            }
            else
            {
                WarningNotValid.IsVisible = false;
            }
            AssignCurrentDetails();
        }
        private void ChangeRound(object sender, EventArgs e)
        {
            var converter = new ColorTypeConverter();
            if (sender == r1select)
            {
                Round = 1;
                r1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                r2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r1select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else
            {
                Round = 2;
                r2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                r1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r2select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
        }

        public void CheckIfValid()
        {
            if(Round != 0 && FieldNum != 0)
            {
                NextPageButton.IsEnabled = true;
            }
        }

        private void rerunCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Rerun = e.Value;
        }

        private void usableCheckbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Usable = e.Value;
        }
        async private void SetUpSignalR()
        {
            MasterServerConnection();
            await SignalRConnect();
            await GetFields();
        }
        private void MasterServerConnection()
        {
            var ip = "localhost";
            if (hubConnection == null || hubConnection.State != HubConnectionState.Connected)
            {
                hubConnection = new HubConnectionBuilder().WithUrl($"http://24.35.25.72:80/scoreHub").Build();
            }

            hubConnection.On<int[], string[], int[], bool[], bool[], bool[]>("fieldDefaults", (teamids, teamnumbers, rounds, reruns, tests, validate) =>
            {
                int index = 0;
                foreach(var item in teamids)
                {
                    TeamIDs[index] = item;
                    index++;
                }
                index = 0;
                foreach (var item in teamnumbers)
                {
                    TeamNumbers[index] = item;
                    index++;
                }
                index = 0;
                foreach (var item in rounds)
                {
                    Rounds[index] = item;
                    index++;
                }
                index = 0;
                foreach (var item in reruns)
                {
                    Reruns[index] = item;
                    index++;
                }
                index = 0;
                foreach (var item in tests)
                {
                    Tests[index] = item;
                    index++;
                }
                index = 0;
                foreach (var item in validate)
                {
                    Validates[index] = item;
                    index++;
                }
                Device.BeginInvokeOnMainThread(() => {
                    var converter = new ColorTypeConverter();
                    f1select.Text = "Field 1 (" + TeamNumbers[0] + ")";
                    f2select.Text = "Field 2 (" + TeamNumbers[1] + ")";
                    f3select.Text = "Field 3 (" + TeamNumbers[2] + ")";
                    f4select.Text = "Field 4 (" + TeamNumbers[3] + ")";
                    f5select.Text = "Field 5 (" + TeamNumbers[4] + ")";
                    f6select.Text = "Field 6 (" + TeamNumbers[5] + ")";
                    f1select.IsEnabled = true;
                    f2select.IsEnabled = true;
                    f3select.IsEnabled = true;
                    f4select.IsEnabled = true;
                    f5select.IsEnabled = true;
                    f6select.IsEnabled = true;
                    if (Validates[0])
                    {
                        f1select.Style = (Style)Resources["judgePositive"] as Style;
                    }
                    else
                    {
                        f1select.Style = (Style)Resources["judgeInvalid"] as Style;
                    }
                    if (Validates[1])
                    {
                        f2select.Style = (Style)Resources["judgePositive"] as Style;
                    }
                    else
                    {
                        f2select.Style = (Style)Resources["judgeInvalid"] as Style;
                    }
                    if (Validates[2])
                    {
                        f3select.Style = (Style)Resources["judgePositive"] as Style;
                    }
                    else
                    {
                        f3select.Style = (Style)Resources["judgeInvalid"] as Style;
                    }
                    if (Validates[3])
                    {
                        f4select.Style = (Style)Resources["judgePositive"] as Style;
                    }
                    else
                    {
                        f4select.Style = (Style)Resources["judgeInvalid"] as Style;
                    }
                    if (Validates[4])
                    {
                        f5select.Style = (Style)Resources["judgePositive"] as Style;

                    }
                    else
                    {
                        f5select.Style = (Style)Resources["judgeInvalid"] as Style;
                    }
                    if (Validates[5])
                    {
                        f6select.Style = (Style)Resources["judgePositive"] as Style;
                    }
                    else
                    {
                        f6select.Style = (Style)Resources["judgeInvalid"] as Style;
                    }

                });
                
            });

        }
        private void AssignCurrentDetails()
        {
            var converter = new ColorTypeConverter();
            if (Rounds[SelectedIndex] == 1)
            {
                Round = 1;
                r1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                r2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r1select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else
            {
                Round = 2;
                r2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                r1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r2select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                r1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            rerunCheckbox.IsChecked = Reruns[SelectedIndex];
            CheckIfValid();
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
        async Task GetFields()
        {
            try
            {
                await hubConnection.InvokeAsync("judgeClientConnection", CompID);
            }
            catch (Exception ex)
            {
                f1select.Text = "Send Failed";
            }
        }
    }
}
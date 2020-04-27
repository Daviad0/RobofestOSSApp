using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
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
        private static int[] Rounds = new int[6];
        private static bool[] Reruns = new bool[6];
        private static bool[] Tests = new bool[6];
        private static int SelectedIndex = 0;
        public FieldSelectionPage()
        {
            InitializeComponent();
            SetUpSignalR();
        }
        private void NextPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckSelectionPage(1));
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
            if (sender == f1select)
            {
                FieldNum = 1;
                SelectedIndex = 0;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if(sender == f2select)
            {
                FieldNum = 2;
                SelectedIndex = 1;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if(sender == f3select)
            {
                FieldNum = 3;
                SelectedIndex = 2;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == f4select)
            {
                FieldNum = 4;
                SelectedIndex = 3;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == f5select)
            {
                FieldNum = 5;
                SelectedIndex = 4;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
            }
            else if (sender == f6select)
            {
                FieldNum = 6;
                SelectedIndex = 5;
                f1select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f6select.BackgroundColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f3select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f4select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f5select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f2select.BackgroundColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f1select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f6select.TextColor = (Color)converter.ConvertFromInvariantString("#ffffff");
                f3select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f4select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f5select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
                f2select.TextColor = (Color)converter.ConvertFromInvariantString("Color.DodgerBlue");
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
                hubConnection = new HubConnectionBuilder().WithUrl($"http://robofest.daviadoprojects.codes/scoreHub").Build();
            }

            hubConnection.On<int[], string[], int[], bool[], bool[]>("fieldDefaults", (teamids, teamnumbers, rounds, reruns, tests) =>
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
                Device.BeginInvokeOnMainThread(() => {
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
                await hubConnection.InvokeAsync("judgeClientConnection");
            }
            catch (Exception ex)
            {
                f1select.Text = "Send Failed";
            }
        }
    }
}
using RobofestApp.Models;
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
    public partial class CheckSelectionPage : ContentPage
    {
        private static int FieldLoaded;
        private static TeamDataStorage teamData = new TeamDataStorage();
        public CheckSelectionPage(TeamDataStorage getData)
        {
            teamData = getData;
            FieldLoaded = getData.Field;
            InitializeComponent();
            Title = "Field " + teamData.Field.ToString() + ": " + teamData.TeamNumber;
            round.Text = "Round " + teamData.Round.ToString();
            field.Text = "Field " + teamData.Field.ToString();
            if (teamData.Rerun)
            {
                rerun.IsVisible = true;
            }
            if (teamData.Test)
            {
                test.IsVisible = true;
            }
            if (!teamData.Valid)
            {
                valid.IsVisible = true;
            }
        }

        private void Information_Correct(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotReadyPage(teamData));
        }
        private void Information_Incorrect(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FieldSelectionPage());
        }
    }
}
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
    public partial class CheckScore : ContentPage
    {
        private static TeamMatchStorage currentTeamMatch = new TeamMatchStorage();
        public CheckScore(TeamMatchStorage teamMatch)
        {
            InitializeComponent();
            currentTeamMatch = teamMatch;
        }
        protected override void OnAppearing()
        {
            var converter = new ColorTypeConverter();
            base.OnAppearing();
            totalScore.Text = "Total Score: " + currentTeamMatch.TotalScore.ToString();
            //BOTTLE SCORES
            bottleScore.Text = "Bottle Score: " + currentTeamMatch.BottleScores.Sum().ToString();
            bot1score.Text = currentTeamMatch.BottleScores[0].ToString();
            if (currentTeamMatch.BottleScores[0] > 0)
            {
                bot1img.Source = ImageSource.FromFile("bottlepos.png");
            }
            else
            {
                bot1img.Source = ImageSource.FromFile("bottlereg.png");
            }
            bot2score.Text = currentTeamMatch.BottleScores[1].ToString();
            if (currentTeamMatch.BottleScores[1] > 0)
            {
                bot2img.Source = ImageSource.FromFile("bottlepos.png");
            }
            else
            {
                bot2img.Source = ImageSource.FromFile("bottlereg.png");
            }
            bot3score.Text = currentTeamMatch.BottleScores[2].ToString();
            if (currentTeamMatch.BottleScores[2] > 0)
            {
                bot3img.Source = ImageSource.FromFile("bottlepos.png");
            }
            else
            {
                bot3img.Source = ImageSource.FromFile("bottlereg.png");
            }
            bot4score.Text = currentTeamMatch.BottleScores[3].ToString();
            if (currentTeamMatch.BottleScores[3] > 0)
            {
                bot4img.Source = ImageSource.FromFile("bottlepos.png");
            }
            else
            {
                bot4img.Source = ImageSource.FromFile("bottlereg.png");
            }
            bot5score.Text = currentTeamMatch.BottleScores[4].ToString();
            if (currentTeamMatch.BottleScores[4] < 0)
            {
                bot5img.Source = ImageSource.FromFile("bottleneg.png");
            }
            else
            {
                bot5img.Source = ImageSource.FromFile("bottlereg.png");
            }
            //BALL SCORES
            ballScore.Text = "Ball Score: " + currentTeamMatch.BallScores.Sum().ToString();
            //WHITE BALLS
            var numwhiteballs = currentTeamMatch.BallScores[0] / 15;
            if(numwhiteballs >= 1)
            {
                whiteb1.Source = ImageSource.FromFile("whiteyes.png");
            }
            if (numwhiteballs >= 2)
            {
                whiteb2.Source = ImageSource.FromFile("whiteyes.png");
            }
            if (numwhiteballs == 3)
            {
                whiteb3.Source = ImageSource.FromFile("whiteyes.png");
            }
            //ORANGE BALLS
            var numorangeballs = currentTeamMatch.BallScores[1] / 18;
            if (numorangeballs >= 1)
            {
                orangeb1.Source = ImageSource.FromFile("orangeyes.png");
            }
            if (numorangeballs == 2)
            {
                orangeb2.Source = ImageSource.FromFile("orangeyes.png");
            }
            //INVALID BALLS
            var numinvalidballs = currentTeamMatch.BallScores[2] / -3;
            if (numinvalidballs >= 1)
            {
                invalidb1.Source = ImageSource.FromFile("invalidyes.png");
            }
            if (numinvalidballs >= 2)
            {
                invalidb2.Source = ImageSource.FromFile("invalidyes.png");
            }
            if (numinvalidballs >= 3)
            {
                invalidb3.Source = ImageSource.FromFile("invalidyes.png");
            }
            if (numinvalidballs >= 4)
            {
                invalidb4.Source = ImageSource.FromFile("invalidyes.png");
            }
            if (numinvalidballs == 5)
            {
                invalidb5.Source = ImageSource.FromFile("invalidyes.png");
            }
            gameScore.Text = "GAME Score: " + (currentTeamMatch.GAMEScores[0] + currentTeamMatch.GAMEScores[1]).ToString();
            //GAME SCORES
            if(currentTeamMatch.GAMEScores[0] > 0)
            {
                endgame.BackgroundColor = (Color)converter.ConvertFromInvariantString("#15d656");
                endgame.BorderColor = (Color)converter.ConvertFromInvariantString("#15d656");
            }
            if (currentTeamMatch.GAMEScores[1] > 0)
            {
                robotIntact.BackgroundColor = (Color)converter.ConvertFromInvariantString("#15d656");
                robotIntact.BorderColor = (Color)converter.ConvertFromInvariantString("#15d656");
            }
            resetScore.Text = "Reset Penalty: " + currentTeamMatch.FieldReset.ToString();
        }

        private void SubmitScores_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }
    }
}
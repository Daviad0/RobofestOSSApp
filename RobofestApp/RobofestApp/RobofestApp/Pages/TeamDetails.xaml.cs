using Newtonsoft.Json;
using RobofestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobofestApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetails : ContentPage
    {
        public TeamDetails(string TeamNumber)
        {
            InitializeComponent();
            BindingContext = new TeamViewModel(TeamNumber);
            var json = "";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://192.168.86.59/team/");
                HttpResponseMessage response = client.GetAsync("RawTeamData?TeamNumber=" + TeamNumber).Result;
                if (response.Content.ReadAsStringAsync().Result != "")
                {
                    response.EnsureSuccessStatusCode();
                    json = response.Content.ReadAsStringAsync().Result;
                    var teamdata = JsonConvert.DeserializeObject<RawTeamData>(json);
                    Title = teamdata.TeamName + " (" + teamdata.TeamNumber + ")";
                    TeamLocation.Text = "From " + teamdata.Location;
                    TeamCoach.Text = "Coached by " + teamdata.Coach;
                    NumEntries.Text = "Team " + teamdata.TeamNumber + " has " + teamdata.RoundEntries.Count() + " entries";
                }
            }
        }
    }
}
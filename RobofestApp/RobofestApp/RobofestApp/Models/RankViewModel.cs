using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Http;

namespace RobofestApp.Models
{
    public class RankViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<TeamRank> ranks;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TeamRank> Ranks
        {
            get { return ranks; }
            set
            {

                ranks = value;
            }
        }


        public RankViewModel()
        {
            var json = "";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://192.168.86.59/RobofestWTECore/team/");
                HttpResponseMessage response = client.GetAsync("RawLeaderboard").Result;
                response.EnsureSuccessStatusCode();
                json = response.Content.ReadAsStringAsync().Result;
            }
            var ranklist = JsonConvert.DeserializeObject<List<ReturnRank>>(json);

            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            Ranks = new ObservableCollection<TeamRank>();
            foreach (var jsonranking in ranklist)
            {
                var newrank = new TeamRank();
                newrank.AverageScore = jsonranking.AverageScore;
                newrank.Ranking = jsonranking.Rank;
                newrank.TeamNumber = jsonranking.TeamNumber;
                Ranks.Add(newrank);
            }

        }
    }
}

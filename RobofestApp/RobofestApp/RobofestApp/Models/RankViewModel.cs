using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Http;
using RestSharp;
using System.Threading.Tasks;

namespace RobofestApp.Models
{
    public class RankViewModel : INotifyPropertyChanged
    {

        private static ObservableCollection<TeamRank> ranks = new ObservableCollection<TeamRank>();
        private static HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TeamRank> Ranks
        {
            get { return ranks; }
            set
            {

                ranks = value;
            }
        }


        public async Task Update()
        {
            var json = "";
            //using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            //{
                client.DefaultRequestHeaders.ConnectionClose = true;
                
                //client.Timeout = TimeSpan.FromMinutes(20);
                //client.BaseAddress = new Uri("http://robofest.daviadoprojects.codes/team/");
                HttpResponseMessage response = await client.GetAsync("http://robofest.daviadoprojects.codes/team/RawLeaderboard").ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                json = response.Content.ReadAsStringAsync().Result;

            //}
            var ranklist = JsonConvert.DeserializeObject<List<ReturnRank>>(json);

            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            //Ranks = new ObservableCollection<TeamRank>();
            Ranks.Clear();
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

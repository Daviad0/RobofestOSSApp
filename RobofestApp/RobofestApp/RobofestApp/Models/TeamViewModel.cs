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
    public class TeamViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<RawRound> rawRounds;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<RawRound> RawRounds
        {
            get { return rawRounds; }
            set
            {

                rawRounds = value;
            }
        }


        public TeamViewModel(string TeamNumber)
        {
            var json = "";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://24.35.25.72:80/team/");
                HttpResponseMessage response = client.GetAsync("RawTeamData?TeamNumber=" + TeamNumber).Result;
                if(response.Content.ReadAsStringAsync().Result != "")
                {
                    response.EnsureSuccessStatusCode();
                    json = response.Content.ReadAsStringAsync().Result;
                    var ranklist = JsonConvert.DeserializeObject<RawTeamData>(json);
                    RawRounds = new ObservableCollection<RawRound>();
                    foreach (var item in ranklist.RoundEntries)
                    {
                        RawRounds.Add(item);
                    }
                }
            }
            
            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            

        }
    }
}

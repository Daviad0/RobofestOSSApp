using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RobofestApp.Models
{
    public class CompetitionViewModel
    {
        private ObservableCollection<CompetitionModel> comps;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CompetitionModel> Comps
        {
            get { return comps; }
            set
            {

                comps = value;
            }
        }
        

        public CompetitionViewModel()
        {
            var json = "";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.Timeout = TimeSpan.FromSeconds(5);
                client.BaseAddress = new Uri("http://robofest.daviadoprojects.codes/team/");
                HttpResponseMessage response = client.GetAsync("RawCompetitions").Result;
                response.EnsureSuccessStatusCode();
                json = response.Content.ReadAsStringAsync().Result;

            }
            var schedulelist = JsonConvert.DeserializeObject<List<CompetitionModel>>(json);

            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            Comps = new ObservableCollection<CompetitionModel>();
            foreach (var jsonitem in schedulelist)
            {
                var newcomp = new CompetitionModel();
                newcomp = jsonitem;
                Comps.Add(newcomp);
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobofestApp.Models
{
    public class CompetitionViewModel
    {
        public static ObservableCollection<CompetitionModel> comps = new ObservableCollection<CompetitionModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CompetitionModel> Comps
        {
            get { return comps; }
            set
            {

                comps = value;
            }
        }
        

        public async Task Update()
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
            Comps.Clear();
            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            foreach (var jsonitem in schedulelist)
            {
                var newcomp = new CompetitionModel();
                newcomp = jsonitem;
                Comps.Add(newcomp);
            }
        }
    }
}

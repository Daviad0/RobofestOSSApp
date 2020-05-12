using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace RobofestApp.Models
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {

        private static ObservableCollection<RawScheduleItem> items = new ObservableCollection<RawScheduleItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<RawScheduleItem> Items
        {
            get { return items; }
            set
            {

                items = value;
            }
        }


        public async Task Update(int CompID)
        {
            var json = "";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.Timeout = TimeSpan.FromSeconds(5);
                client.BaseAddress = new Uri("http://24.35.25.72:80/team/");
                HttpResponseMessage response = client.GetAsync("RawSchedule/" + CompID.ToString()).Result;
                response.EnsureSuccessStatusCode();
                json = response.Content.ReadAsStringAsync().Result;
                
            }
            var schedulelist = JsonConvert.DeserializeObject<List<RawScheduleItem>>(json);

            // Here you can have your data form db or something else,
            // some data that you already have to put in the list
            Items.Clear();
            foreach (var jsonitem in schedulelist)
            {
                var match = new RawScheduleItem();
                match.Completed = jsonitem.Completed;
                match.OrderNum = jsonitem.OrderNum;
                match.Round = jsonitem.Round;
                match.Valid = jsonitem.Valid;
                match.TeamNumber = jsonitem.TeamNumber;
                match.TestMatch = jsonitem.TestMatch;
                if(match.Completed == true)
                {
                    match.CompletedColor = "#24bd7d";
                }
                else
                {
                    match.CompletedColor = "Gray";
                }
                if (match.Valid == true)
                {
                    match.Color = "Transparent";
                }
                else
                {
                    if(match.TeamNumber == "EMPTY")
                    {
                        match.Color = "#ff442b";
                    }
                    else
                    {
                        match.Color = "#ffc526";
                    }
                    
                }
                Items.Add(match);
            }

        }
    }
}

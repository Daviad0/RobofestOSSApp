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
    public class UpNowViewModel : INotifyPropertyChanged
    {

        private static ObservableCollection<TeamUpNow> teams = new ObservableCollection<TeamUpNow>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TeamUpNow> Teams
        {
            get { return teams; }
            set
            {

                teams = value;
            }
        }


        public async Task Update(List<StaticField> staticFields)
        {
            Teams.Clear();
            int i = 1;
            foreach(var item in staticFields)
            {
                TeamUpNow teamUpNow = new TeamUpNow();
                teamUpNow.Field = i;
                teamUpNow.Round = item.Round;
                teamUpNow.TeamNumber = item.TeamNumber;
                i++;
                Teams.Add(teamUpNow);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class RawTeamData
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public string TeamNumber { get; set; }
        public string Coach { get; set; }
        public string Location { get; set; }
        public List<RawRound> RoundEntries { get; set; } = new List<RawRound>();
        public float AverageScore { get; set; }
    }
}

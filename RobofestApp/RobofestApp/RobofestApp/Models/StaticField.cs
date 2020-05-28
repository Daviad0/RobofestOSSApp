using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class StaticField
    {
        public int CurrentScore { get; set; }
        public int Round { get; set; }
        public bool Rerun { get; set; }
        public bool Test { get; set; }
        public int TeamID { get; set; }
        public string TeamNumber { get; set; }
        public string Data { get; set; }
        public string ClientConnectionID { get; set; }
        public int Status { get; set; }
        public string FieldConnectionID { get; set; }
        public bool HelpNeeded { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class CompetitionModel
    {
        public int CompID { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int RunningState { get; set; }
        public bool SetUp { get; set; }
    }
}

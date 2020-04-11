using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class ReturnRank
    {
        public int Rank { get; set; }
        public string TeamNumber { get; set; }
        public float AverageScore { get; set; }
        public int Round1Score { get; set; }
        public int Round2Score { get; set; }
    }
}

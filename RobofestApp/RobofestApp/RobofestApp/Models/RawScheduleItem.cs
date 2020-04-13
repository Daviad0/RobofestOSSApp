using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class RawScheduleItem
    {
        public int OrderNum { get; set; }
        public string TeamNumber { get; set; }
        public bool Valid { get; set; }
        public int Round { get; set; }
        public bool Completed { get; set; }
        public bool TestMatch { get; set; }
        public string Color { get; set; }
        public string CompletedColor { get; set; }
    }
}

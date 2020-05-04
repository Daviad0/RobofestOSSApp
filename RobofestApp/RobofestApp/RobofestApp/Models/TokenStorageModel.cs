using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp.Models
{
    public class TokenStorageModel
    {
        public string SessionID { get; set; }
        public string AuthToken { get; set; }
        public int CompID { get; set; }
    }
}

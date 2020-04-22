using System;
using System.Collections.Generic;
using System.Text;

namespace RobofestApp
{
    public interface TokenStorage
    {
        string TokenValue(string tokentostore, string sessionid);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using Newtonsoft.Json;
using RobofestApp.Models;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(RobofestApp.iOS.TokenImplementation))]
namespace RobofestApp.iOS
{
    public class TokenImplementation:TokenStorage
    {
        public string TokenValue(string tokentostore, string session)
        {
            var objecttostore = new TokenStorageModel();
            objecttostore.SessionID = session;
            objecttostore.AuthToken = tokentostore;
            var jsontostore = JsonConvert.SerializeObject(objecttostore);
            string folderName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(folderName, "token.txt");
            File.WriteAllText(filePath, jsontostore);
            return filePath;
        }
    }
}
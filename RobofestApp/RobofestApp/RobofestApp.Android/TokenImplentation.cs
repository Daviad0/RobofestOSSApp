using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using RobofestApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(RobofestApp.Droid.TokenImplentation))]
namespace RobofestApp.Droid
{
    public class TokenImplentation:TokenStorage
    {
        public string TokenValue(string tokentostore, string session)
        {
            var objecttostore = new TokenStorageModel();
            objecttostore.AuthToken = tokentostore;
            objecttostore.SessionID = session;
            var jsontostore = JsonConvert.SerializeObject(objecttostore);
            var docpath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            try
            {
                System.IO.Directory.CreateDirectory(Path.Combine(docpath, "Robofest"));
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent(ex.Message);
            }
            docpath = Path.Combine(docpath, "Robofest");
            var filePath = Path.Combine(docpath, "token.txt");
            try
            {
                System.IO.File.WriteAllText(filePath, jsontostore);
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent(ex.Message);
            }
            return filePath;
        }
    }
}
using Newtonsoft.Json;
using RobofestApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RobofestApp.Scripts
{
    public class TokenStorageMaster
    {
        public string StoreToken(string token, string session)
        {
            try
            {
                var objecttostore = new TokenStorageModel();
                objecttostore.AuthToken = token;
                objecttostore.SessionID = session;
                var jsontostore = JsonConvert.SerializeObject(objecttostore);
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "token.txt");
                File.WriteAllText(fileName, jsontostore);
                return "Success";
            }
            catch(Exception ex)
            {
                return "Failure Storing";
            }
            
            
        }
        public TokenStorageModel GetToken()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "token.txt");
                var filestring = File.ReadAllText(fileName);
                var objectgotten = JsonConvert.DeserializeObject<TokenStorageModel>(filestring);
                return objectgotten;
            }
            catch (Exception ex)
            {
                var objectgotten = new TokenStorageModel();
                return objectgotten;
            }


        }
    }
}

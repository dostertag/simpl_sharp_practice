using System;
using System.Text;
using Crestron.SimplSharp;
using System.Net;
using Newtonsoft.Json;
using Crestron.SimplSharp.CrestronIO;                          				// For Basic SIMPL# Classes

namespace ChuckQuotes
{
    public class ChuckJoke
    {

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public ChuckJoke()
        {
        }

        public string GetRandomJoke()
        {
            string jsonResponse = string.Empty;
            string uri = @"http://api.icndb.com/jokes/random";

            jsonResponse = Get(uri);
            ChuckSays currentChuck = deserializeJSON(jsonResponse);
            return currentChuck.value.joke;

            
        }

        private static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }

        }

        private static ChuckSays deserializeJSON(string json)
        {
            try
            {
                ChuckSays response = JsonConvert.DeserializeObject<ChuckSays>(json);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating Chuck: " + ex.Message.ToString());

            }
            return null;
        }
    }
}

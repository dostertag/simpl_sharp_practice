using System;
using System.Text;
using Crestron.SimplSharp;
using System.Net;
using Newtonsoft.Json;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp.Net.Http;                          				// For Basic SIMPL# Classes

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
            string uri = @"http://api.icndb.com/jokes/random";
            return GetAJoke(uri);
            
            
        }
        /// <summary>
        /// sample code found here:
        /// https://github.com/plinck/CrestronSSClass/blob/master/SimplSharpDay3_Json/SimplSharpDay3_Json/ControlSystem.cs
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static string GetAJoke(string uri)
        {
            string response = string.Empty;
            HttpClient client = new HttpClient();
            HttpClientResponse httpResponse;
            HttpClientRequest httpRequest = new HttpClientRequest();
            client.TimeoutEnabled = true;
            client.Timeout = 5;
            client.KeepAlive = false;
            httpRequest.Url.Parse(uri);
            httpResponse = client.Dispatch(httpRequest);

            ChuckSays currentChuck = deserializeJSON(httpResponse.ContentString);
            
            return currentChuck.value.joke;
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
                CrestronConsole.ConsoleCommandResponse("Error creating chuck!");
                return null;                
            }
        }
    }
}

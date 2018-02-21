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

        public string randomJoke;

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public ChuckJoke()
        {
        }
        /*
        public string GetRandomJoke()
        {
            string chucksURL = @"http://api.icndb.com/jokes/random.json";
            string randomJoke = String.Empty;
            randomJoke = GetAJoke(chucksURL);
            return returnedJoke;
            
            
        }
         * */
        /// <summary>
        /// sample code found here:
        /// https://github.com/plinck/CrestronSSClass/blob/master/SimplSharpDay3_Json/SimplSharpDay3_Json/ControlSystem.cs
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public void GetAJoke()
        {
            string chucksURL = @"http://api.icndb.com/jokes/random.json";

            try
            {
                var httpSet = new HttpClient();
                httpSet.KeepAlive = false;
                httpSet.Accept = "text/html";
                HttpClientRequest sRequest = new HttpClientRequest();
                sRequest.Url.Parse(chucksURL);
                HttpClientResponse sResponse = httpSet.Dispatch(sRequest);
                var jsontext = sResponse.ContentString;

                ChuckSays currentChuck = JsonConvert.DeserializeObject<ChuckSays>(jsontext);
                randomJoke = currentChuck.value.joke;

            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("couldn't execute: {0}", e);
            }

            /*
            string response = string.Empty;
            HttpClient client = new HttpClient();
            // response = client.Get(uri);
            CrestronConsole.Print(response);
            HttpClientResponse httpResponse;
            HttpClientRequest httpRequest = new HttpClientRequest();
            client.TimeoutEnabled = true;
            client.Timeout = 5;
            client.KeepAlive = false;
            httpRequest.Url.Parse(someURL);
            httpResponse = client.Dispatch(httpRequest);

            ChuckSays currentChuck = deserializeJSON(httpResponse.ContentString);
            
            return currentChuck.value.joke;
             * */
        }


        private static ChuckSays deserializeJSON(string json)
        {
            try
            {
                ChuckSays response = JsonConvert.DeserializeObject<ChuckSays>(json);
                string tmp = response.value.joke;
                CrestronConsole.ConsoleCommandResponse(tmp);
                return response;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("Error creating chuck! {0}", ex);
                return null;                
            }
        }
    }
    
}

using System;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;                          				// For Basic SIMPL# Classes

namespace WeatherUnderground
{
    public class Forecast
    {
        public string userKey { get; set; }
        private string url;

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public Forecast()
        {
        }

        public void setKey(string key)
        {
            userKey = key;
        }

        public void getForecast()
        {
            string jsonString = String.Empty;
            url = @"http://api.wunderground.com/api" + userKey + "/forecast/q/TX/Dallas.json";

            var httpSet = new HttpClient();
            httpSet.KeepAlive = false;
            httpSet.Accept = "text/html";
            HttpClientRequest clientRequest = new HttpClientRequest();
            clientRequest.Url.Parse(url);
        }

    }
}

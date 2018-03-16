using System;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net.Http;                          				// For Basic SIMPL# Classes
using Newtonsoft.Json;
using WeatherUnderground.Models.Forecast;
using System.Collections.Generic;

namespace WeatherUnderground
{
    public class Forecast
    {
        public string userKey { get; set; }
        private string url;
        private ForecastObject forecastObject;
        public string[] forecastStrings;
        public string[] forecastIcons;
        public string[] forecastTitles;
        public string errorToSimpl;
        public event EventHandler completeForecastUpdate;

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public Forecast()
        {
        }

        public void setUserKey(string key)
        {
            userKey = key;
        }

         

        public void initializeForecast()
        {
            try
            {
                string jsonString = String.Empty;
                url = string.Format("http://api.wunderground.com/api{0}/forecast/q/TX/Dallas.json", userKey);
                var httpClient = new HttpClient();
                httpClient.KeepAlive = false;
                httpClient.Accept = "text/html";
                HttpClientRequest httpRequest = new HttpClientRequest();
                httpRequest.Url.Parse(url);

                HttpClientResponse rResponse = httpClient.Dispatch(httpRequest);
                var responseString = rResponse.ContentString;

                forecastObject = JsonConvert.DeserializeObject<ForecastObject>(responseString);
                
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine("Error making request: {0}", e);
            }



        }

        public void getForecast()
        {
            try
            {
                List<string> tempForecastStrings = new List<string>();
                List<string> tempForecastImageUrls = new List<string>();
                List<string> tempForecastTitles = new List<string>();
                foreach (Forecastday fDay in forecastObject.forecast.txt_forecast.forecastday)
                {
                    tempForecastStrings.Add(fDay.fcttext);
                    tempForecastImageUrls.Add(fDay.icon_url);
                    tempForecastTitles.Add(fDay.title);
                }
                forecastStrings = tempForecastStrings.ToArray();
                forecastIcons = tempForecastImageUrls.ToArray();
                forecastTitles = tempForecastTitles.ToArray();
                triggerForecastSuccess();
            }
            catch (Exception e)
            {
                errorToSimpl = string.Format("Error creating lists: {0}", e);
                CrestronConsole.PrintLine("Error creating string arrays: {0}", e);
                triggerErrorToSimpl();

            }
        }
        public void triggerErrorToSimpl()
        { 
            
        }

        public void triggerForecastSuccess()
        {
            completeForecastUpdate(this, new EventArgs());
        }


        public int getNumberOfDays()
        {
            return forecastObject.forecast.txt_forecast.forecastday.Count;
        }

    }
}

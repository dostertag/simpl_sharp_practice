using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WeatherUnderground.Models.Forecast;

namespace WeatherUnderground
{
    class Program
    {

        // private SingleResult singleResult;

        static void Main(string[] args)
        {
             
            string html = string.Empty;
            // string uRL = @"http://api.wunderground.com/api/53fbb860e4fa3b14/conditions/q/TX/Dallas.json";
            string uRLForecast = @"http://api.wunderground.com/api/53fbb860e4fa3b14/forecast/q/TX/Dallas.json";

            html = get(uRLForecast);
            // Console.WriteLine(html);
            ForecastObject tmpForecast = JsonConvert.DeserializeObject<ForecastObject>(html);

            foreach (Forecastday currentDay in tmpForecast.forecast.txt_forecast.forecastday)
            {
                Console.WriteLine(currentDay.icon_url);
            }
             
            Console.ReadKey();
            
        }

        private static string get(string uri)
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


        ///
        /// someone suggested JSON.Stringify(obj)
        /// ///
        /// ///
        /// 



        /*
        private T GetFirstInstance<T>(string propertyName, string json)
        {
            using (var stringReader = new StringReader(json))
                using(var jsonReader = new JsonTextReader(stringReader))
            {
                while (jsonReader.Read())
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName && (string)jsonReader.Value == propertyName)
                    {
                        jsonReader.Read();
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<T>(jsonReader);
                    }
                }
                return default(T);
            }
        }

        
        private static IList<SingleResult> parseJsonIntoObject(string jsonString)
        {
            JObject genericResultClass = JObject.Parse(jsonString);

            IList<JToken> results = genericResultClass.Children().ToList();

            IList<SingleResult> tempList = new List<SingleResult>();

            foreach (JToken token in results)
            {

                SingleResult singleResult = token.ToObject<SingleResult>();
                tempList.Add(singleResult);
            }
            return tempList;
        }
        */
    }
}

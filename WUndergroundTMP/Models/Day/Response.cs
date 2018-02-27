using WeatherUnderground.Models;

namespace WeatherUnderground.DataClasses
{
    public class Response
    {
        public string version { get; set; }
        public string termsofService { get; set; }
        public Features features { get; set; }
    }
}

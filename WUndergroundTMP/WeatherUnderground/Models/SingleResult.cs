using WeatherUnderground.DataClasses;

namespace WeatherUnderground
{
    // [DataContract]
    class SingleResult
    {
        public Response response { get; set; }
        public CurrentObservation current_observation { get; set; }
    }
}

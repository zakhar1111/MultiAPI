namespace Weather.API.Services
{
    public class WeatherServiceOptions
    {
        public const string EndPoint = "EndPoint";
        public string APPID { get; set; }
        public string Url { get; set; }
        public string CityLocation { get; set; }

    }
}

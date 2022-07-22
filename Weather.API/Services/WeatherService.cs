using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.API.Exceptions;
using Weather.API.Model;

namespace Weather.API.Services
{
    public class WeatherServiceOptions
    {
        public const string EndPoint = "EndPoint";
        public string APPID { get; set; }
        public string Url { get; set; }
        public string CityLocation { get;set; }

    }

    public class WeatherService : IWeatherService
    {
        private HttpClient _httpClient;
        private readonly WeatherServiceOptions serviceOptions;

        public WeatherService(
            HttpClient httpClient,
            IOptions<WeatherServiceOptions> options
            )
        { 
            serviceOptions = options.Value;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(serviceOptions.Url);

        }
        public async Task<WeatherObject> Get()
        {
            return await Get(serviceOptions.CityLocation);
        }

        public async Task<WeatherObject> Get(string city)
        {//TODO - add mapping RootWeather --> WeatherObject -- see 21 Rahul
            //TODO - put Root object that return Weather[]

            string APIURL = $"?q={city}&appid={serviceOptions.APPID}&units=metric&cnt=1";

            //TODO - var _httpClient = _httpClientFactory.CreateClient();
            var response = await _httpClient.GetAsync(APIURL);

            EnsureSuccess(response.StatusCode);

            var content =  await response.Content.ReadAsStringAsync();//TODO - replace by GetStringAsync()
            var result = JsonConvert.DeserializeObject<WeatherObject>(content);
            return result;
        }

        private static void EnsureSuccess(HttpStatusCode statusCode)
        {//TODO: Refactor it seems this code should be removed to somewhere
            //TODO - logic should check city == null || empty
            if (statusCode == HttpStatusCode.BadRequest)
                throw new WeatherBadRequestException("Bad Request of WeatherService");
            if (statusCode == HttpStatusCode.NotFound)
                throw new WeatherNotFoundException("WeatherService does Not Found city ");
        }
    }
}

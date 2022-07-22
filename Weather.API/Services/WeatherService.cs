using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.API.Exceptions;
using Weather.API.Middlewares;
using Weather.API.Model;

namespace Weather.API.Services
{
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
        {
            //TODO - put Root object that return Weather[]
            string APIURL = $"?q={city}&appid={serviceOptions.APPID}&units=metric&cnt=1";
            var content = await _httpClient.GetStringAsync(APIURL);

            var result = JsonConvert.DeserializeObject<WeatherObject>(content);
            return result;
        }

       
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.API.Exceptions;
using Weather.API.Model;

namespace Weather.API.Services
{
    public class WeatherService : IWeatherService
    {
        private IConfiguration _configRoot;
        private HttpClient _httpClient;

        private string APPID
        {
            get 
            { 
                return _configRoot.GetValue<string>("EndPoint:APPID");
            }
        }
      
        private string DefaultCity
        {
            get 
            { 
                return _configRoot.GetValue<string>("Location:Default");
            }
        }

        public WeatherService(IConfiguration configRoot,HttpClient httpClient)
        {
            _configRoot = configRoot;
            _httpClient = httpClient;
        }
        public async Task<WeatherObject> Get()
        {
            return await Get(DefaultCity);
        }

        public async Task<WeatherObject> Get(string city)
        {
            string APIURL = $"?q={city}&appid={APPID}&units=metric&cnt=1";
            var response = await _httpClient.GetAsync(APIURL);

            EnsureSuccess(response.StatusCode);

            var content =  await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherObject>(content);
        }

        private static void EnsureSuccess(HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.BadRequest)
                throw new WeatherBadRequestException("Bad Request of WeatherService");
            if (statusCode == HttpStatusCode.NotFound)
                throw new WeatherNotFoundException("WeatherService does Not Found city ");
        }
    }
}

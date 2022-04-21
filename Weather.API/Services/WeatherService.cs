using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

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
        public async Task<string> Get()
        {
            string city = DefaultCity;
            string APIURL = $"?q={city}&appid={APPID}&units=metric&cnt=1";
            var response = await _httpClient.GetAsync(APIURL);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Get(string city)
        {
            string APIURL = $"?q={city}&appid={APPID}&units=metric&cnt=1";
            var response = await _httpClient.GetAsync(APIURL);
            return await response.Content.ReadAsStringAsync();
        }

 
    }
}

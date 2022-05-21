using System.Threading.Tasks;
using Weather.API.Model;

namespace Weather.API.Services
{
    public interface IWeatherService
    {
        Task<WeatherObject> Get();
        Task<WeatherObject> Get(string cityName);
    }
}

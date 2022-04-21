using System.Threading.Tasks;

namespace Weather.API.Services
{
    public interface IWeatherService
    {
        Task<string> Get();
        Task<string> Get(string cityName);
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Weather.API.Services;

namespace Weather.API.Helpers
{
    public static class WeatherServiceRegister
    {
        public static void ConfigureWeatherService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IWeatherService, WeatherService>(c =>
            {
                c.BaseAddress = new Uri($"https://api.openweathermap.org/data/2.5/weather");
            });
        }
    }
}

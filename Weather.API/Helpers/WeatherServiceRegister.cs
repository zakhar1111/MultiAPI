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
            services.Configure<WeatherServiceOptions>(configuration.GetSection(WeatherServiceOptions.EndPoint));
            services.AddHttpClient<IWeatherService, WeatherService>();
        }
    }
}

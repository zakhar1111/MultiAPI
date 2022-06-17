using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Time.API.Service;

namespace Time.API.Helpers
{
    public static class TimeServiceRegister
    {
        public static void ConfigureTimeService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ITimeService, TimeService>(c =>
            {
                c.BaseAddress = new Uri(configuration.GetValue<string>("EndPoint:TimeAPI"));
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });
            
        }
    }
}

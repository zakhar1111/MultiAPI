using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.API.Middlewares;
using Weather.API.Services;

namespace Weather.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<WeatherExceptionMiddleware>();
            ConfigureWeatherService(services);
            ConfigureSwaggerService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                UseSwaggerUi(app);
            }

            app.UseRouting();
            app.UseMiddleware<WeatherExceptionMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureWeatherService(IServiceCollection services)
        {//TODO - replace extension method WeatherServiceRegister.Configure(this IServiceCollection services, IConfiguration configuration)
            services.AddHttpClient<IWeatherService, WeatherService>(c =>
            {
                c.BaseAddress = new Uri($"https://api.openweathermap.org/data/2.5/weather");
            });
        }
        private void UseSwaggerUi(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather.API v1"));

        }
        private  void ConfigureSwaggerService(IServiceCollection services)
        {//TODO - replace extension method SwaggerRegister.Configure(this IServiceCollection services)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Weather.API",
                    Version = "v1"
                });
            });
        }
    }
}

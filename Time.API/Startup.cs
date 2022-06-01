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
using Time.API.Middlewares;
using Time.API.Profiles;
using Time.API.Service;

namespace Time.API
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
            ConfigureAutoMapper(services);
            services.AddTransient<TimeExceptionMiddleware>();
            ConfigureTimeService(services);
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
            app.UseMiddleware<TimeExceptionMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void UseSwaggerUi(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books.API v1"));

        }

        private static void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Time.API", 
                    Version = "v1" 
                });
            });
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        { 
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(TimeProfile).Assembly);
        }
        private void ConfigureTimeService(IServiceCollection services)
        {
            services.AddHttpClient<ITimeService, TimeService>(c =>
            {
                c.BaseAddress = new Uri(Configuration.GetValue<string>("EndPoint:TimeAPI"));
                c.DefaultRequestHeaders.Accept.Clear();
                c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });
        }
    }
}

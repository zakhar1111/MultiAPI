using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Weather.API.Helpers
{
    public static class SwaggerRegister
    {
        public static void ConfigureSwaggerService(this IServiceCollection services)
        {
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

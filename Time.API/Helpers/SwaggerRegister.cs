using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Time.API.Helpers
{
    public static class SwaggerRegister
    {
        public static void ConfigureSwaggerService(this IServiceCollection services)
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
    }
}

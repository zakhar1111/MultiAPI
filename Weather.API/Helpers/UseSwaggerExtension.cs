using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Weather.API.Helpers
{
    public static class UseSwaggerExtension
    {
        public static void ConfigureUseSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather.API v1"));
        }
    }
}

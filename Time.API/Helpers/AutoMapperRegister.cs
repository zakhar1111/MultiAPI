using Microsoft.Extensions.DependencyInjection;
using Time.API.Profiles;

namespace Time.API.Helpers
{
    public static class AutoMapperRegister
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TimeProfile).Assembly);
        }
    }
}

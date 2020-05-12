using Microsoft.Extensions.DependencyInjection;
using User.Postgre.Implementations;
using User.Postgre.Interfaces;
using User.Postgre.Models;

namespace User.Postgre
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserDbContext>();
            return services;
        }
    }
}

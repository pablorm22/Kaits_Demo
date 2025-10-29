using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kaits.Application.Interfaces;
using Kaits.Infrastructure.Persistence;

namespace Kaits.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<KaitsDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("KaitsDatabase")));

            services.AddScoped<IKaitsDbContext>(provider => provider.GetRequiredService<KaitsDbContext>());
            return services;
        }
    }
}

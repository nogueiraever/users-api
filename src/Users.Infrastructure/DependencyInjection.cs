using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Infrastructure.Database;

namespace Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersDatabaseContext>(options =>
                       {
                           var connectionString = configuration.GetConnectionString("DefaultConnection");
                           options.UseSqlServer(connectionString);
                       });

            return services;
        }
    }
}

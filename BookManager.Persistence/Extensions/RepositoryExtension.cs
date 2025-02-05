using BookManager.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.Persistence.Extensions
{
    public static class RepositoryExtension
    {

        public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionStringOption = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseNpgsql(connectionStringOption.DefaultConnection, postgreSqlServerAction =>
                    {
                        postgreSqlServerAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                    });
            });

            return services;
        }
    }
}

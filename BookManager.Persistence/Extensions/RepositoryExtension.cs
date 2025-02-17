﻿using BookManager.Application.Contracts.Persistence;
using BookManager.Domain.Options;
using BookManager.Persistence.Authors;
using BookManager.Persistence.Books;
using BookManager.Persistence.Interceptors;
using BookManager.Persistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheOptions = BookManager.Domain.Options.RedisCacheOptions;

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
                options.AddInterceptors(new AuditDbContextInterceptor());
            });

            // Redis cache options configuration
            services.Configure<RedisCacheOptions>(configuration.GetSection("Redis"));

            // Add Redis Cache with the configuration
            services.AddStackExchangeRedisCache(options =>
            {
                var redisOptions = configuration.GetSection("Redis").Get<RedisCacheOptions>();
                options.Configuration = redisOptions.ConnectionString;
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

using System.Reflection;
using BookManager.Application.Features.Authors;
using BookManager.Application.Features.Books;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}

using BookStore.Application.Services;
using BookStore.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationScope(this IServiceCollection services)
    {
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}

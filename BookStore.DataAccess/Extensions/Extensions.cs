using BookStore.Core.Abstractions;
using BookStore.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.DataAccess.Extensions;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(nameof(BookStoreDbContext)))
        );

        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

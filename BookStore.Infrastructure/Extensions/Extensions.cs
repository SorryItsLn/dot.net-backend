using System;
using BookStore.Application.Interfaces.Auth;
using BookStore.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureScope(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
}

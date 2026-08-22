using System.Text;
using BookStore.Application;
using BookStore.Core;
using BookStore.Core.Enums;
using BookStore.Infrastructure;
using BookStore.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.API.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtOptions =
            configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
            ?? throw new InvalidOperationException("JwtOptions are not configured");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey)
                    ),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access"];
                        return Task.CompletedTask;
                    },
                };
            });

        services.AddScoped<IPermissionsService, PermissionsService>();
        services.AddSingleton<IAuthorizationHandler, PermissionsAuthorizationHandler>();

        services.AddAuthorization();
    }

    public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
        this TBuilder builder,
        params Permissions[] permissions
    )
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(police =>
            police.AddRequirements(new PermissionRequirements(permissions))
        );
    }

    public class RequirePermissionsAttribute : AuthorizeAttribute, IAuthorizationRequirementData
    {
        private readonly Permissions[] _permissions;

        public RequirePermissionsAttribute(params Permissions[] permissions)
        {
            _permissions = permissions;
        }

        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return new PermissionRequirements(_permissions);
        }
    }
}

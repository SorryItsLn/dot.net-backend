using BookStore.Application;
using BookStore.Core;
using BookStore.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure;

public class PermissionsAuthorizationHandler : AuthorizationHandler<PermissionRequirements>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionsAuthorizationHandler(IServiceScopeFactory scopeFactory)
    {
        _serviceScopeFactory = scopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirements requirement
    )
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);

        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionsService>();

        var permissions = await permissionService.GetPermissionsAsync(id);

        if (permissions.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}

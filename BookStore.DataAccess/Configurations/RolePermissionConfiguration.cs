using BookStore.Core.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess;

public class RolePermissionConfiguration(AuthorizationOptions authorization)
    : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorizationOptions = authorization;

    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionId });

        var rolePermissions = ParseRolePermissions();

        builder.HasData(rolePermissions);
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        return _authorizationOptions
            .RolePermissions.SelectMany(rp =>
                rp.Permission.Select(p => new RolePermissionEntity
                {
                    RoleId = (int)rp.Role,
                    PermissionId = (int)p,
                })
            )
            .ToArray();
    }
}

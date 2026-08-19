using BookStore.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .HasMany(r => r.Permissions)
            .WithMany(r => r.Roles)
            .UsingEntity<RolePermissionEntity>(
                l => l.HasOne<PermissionsEntity>().WithMany().HasForeignKey(r => r.PermissionId),
                r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(u => u.RoleId)
            );

        var roles = Enum.GetValues<Role>()
            .Select(r => new RoleEntity { Id = (int)r, Name = r.ToString() });

        builder.HasData(roles);
    }
}

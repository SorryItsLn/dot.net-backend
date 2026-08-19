using BookStore.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<PermissionsEntity>
{
    public void Configure(EntityTypeBuilder<PermissionsEntity> builder)
    {
        builder.HasKey(p => p.Id);

        var permissions = Enum.GetValues<Permissions>()
            .Select(p => new PermissionsEntity { Id = (int)p, Name = p.ToString() });

        builder.HasData(permissions);
    }
}

using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess;

public class EmailConfirmationTokenConfiguration
    : IEntityTypeConfiguration<EmailConfirmationTokenEntity>
{
    public void Configure(EntityTypeBuilder<EmailConfirmationTokenEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        ;

        builder.HasIndex(t => t.Token).IsUnique();
    }
}

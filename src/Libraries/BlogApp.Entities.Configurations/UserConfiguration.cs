using Microsoft.EntityFrameworkCore;

namespace BlogApp.Entities.Configurations;

public class UserConfiguration : AuditableEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(x => x.FirstName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Biography).HasMaxLength(1024).IsRequired(false);
        builder.Property(x => x.ProfilePicture).IsRequired(false);
        builder.Property(x => x.Url).HasMaxLength(512).IsRequired(false);
        builder.Property(x => x.IdentityId).IsRequired();
    }
}
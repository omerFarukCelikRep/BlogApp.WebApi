using BlogApp.Entities.Configurations.SeedData;

namespace BlogApp.Entities.Configurations;

public class UserConfiguration : AuditableEntityConfiguration<User>
{
    public const string TableName = "Users";
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.HasIndex(x => x.Email)
               .IsUnique();
        builder.HasIndex(x => x.Url)
               .IsUnique();

        builder.Property(x => x.FirstName)
               .HasMaxLength(256)
               .IsRequired();
        builder.Property(x => x.LastName)
               .HasMaxLength(256)
               .IsRequired();
        builder.Property(x => x.Email)
               .HasMaxLength(256)
               .IsRequired();
        builder.Property(x => x.PasswordHash)
               .HasMaxLength(256)
               .IsRequired();
        builder.Property(x => x.PasswordSalt)
               .HasMaxLength(256)
               .IsRequired();
        builder.Property(x => x.Biography)
               .HasMaxLength(1024)
               .IsRequired(false);
        builder.Property(x => x.ProfilePicture)
               .IsRequired(false);
        builder.Property(x => x.Url)
               .IsRequired(false);

        builder.HasMany<UserRole>()
               .WithOne()
               .HasForeignKey(x => x.UserId);

        builder.HasData(DataGenerator.Users);
    }
}
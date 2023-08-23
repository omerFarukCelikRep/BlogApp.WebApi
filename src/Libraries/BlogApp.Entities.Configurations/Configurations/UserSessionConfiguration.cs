namespace BlogApp.Entities.Configurations.Configurations;
public class UserSessionConfiguration : BaseEntityConfiguration<UserSession>
{
    public const string TableName = "UserSessions";
    public override void Configure(EntityTypeBuilder<UserSession> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.HasIndex(x => x.UserId)
               .IsUnique();

        builder.Property(x => x.IpAddress)
               .HasMaxLength(128);

        builder.HasOne<User>()
               .WithOne()
               .HasForeignKey<UserSession>(x => x.UserId);
    }
}

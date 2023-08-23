namespace BlogApp.Entities.Configurations.Configurations;
public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
{
    public const string TableName = "UserRoles";

    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.HasIndex(x => new { x.UserId, x.RoleId });

        builder.Property(x => x.UserId)
               .IsRequired();
        builder.Property(x => x.RoleId)
               .IsRequired();
    }
}
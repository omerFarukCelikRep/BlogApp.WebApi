namespace BlogApp.Entities.Configurations;
public class RoleConfiguration : AuditableEntityConfiguration<Role>
{
    public const string TableName = "Roles";

    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.Property(x => x.Name)
               .HasMaxLength(256)
               .IsRequired();

        builder.HasMany<UserRole>()
               .WithOne()
               .HasForeignKey(x => x.RoleId);
    }
}
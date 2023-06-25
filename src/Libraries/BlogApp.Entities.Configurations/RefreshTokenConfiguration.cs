namespace BlogApp.Entities.Configurations;

public class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
{
    private const string TableName = "RefreshTokens";
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.Property(x => x.Token)
               .IsRequired();
        builder.Property(x => x.UserId)
               .IsRequired();
        builder.Property(x => x.ExpiryDate)
               .IsRequired();
        builder.Property(x => x.CreatedByIp)
               .IsRequired(false); //TODO:Zorunlu olacak
    }
}
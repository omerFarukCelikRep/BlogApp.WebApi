using BlogApp.Core.Entities.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class RefreshTokenMap : BaseEntityMap<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.ExpiryDate).IsRequired();
        builder.Property(x => x.CreatedByIp).IsRequired(false); //TODO:Zorunlu olacak

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

        base.Configure(builder);
    }
}

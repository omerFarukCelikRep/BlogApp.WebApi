using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class RefreshTokenMap : BaseMap<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.ExpiryDate).IsRequired();
        builder.Property(x => x.CreatedByIp).IsRequired(false); //TODO:Zorunlu olacak

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}

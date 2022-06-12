using BlogApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
public class AuditableEntityMap<TEntity> : BaseEntityMap<TEntity> where TEntity : AuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.DeletedBy).HasMaxLength(128).IsRequired(false);
        builder.Property(x => x.DeletedDate).IsRequired(false);
    }
}

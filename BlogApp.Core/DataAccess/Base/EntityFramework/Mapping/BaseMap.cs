using BlogApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T: BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasAlternateKey(x => x.AutoId);

        builder.Property(x => x.Status).IsRequired(true);

        builder.Property(x => x.CreatedBy).IsRequired(true);
        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        builder.Property(x => x.ModifiedDate).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);
        builder.Property(x => x.DeletedDate).IsRequired(false);

        builder.Property(x => x.AutoId).ValueGeneratedOnAdd();
    }
}

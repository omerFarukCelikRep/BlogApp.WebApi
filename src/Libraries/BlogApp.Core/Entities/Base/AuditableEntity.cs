using BlogApp.Core.Entities.Interfaces;

namespace BlogApp.Core.Entities.Base;
public abstract class AuditableEntity : BaseEntity, IEntity, ICreatableEntity, IUpdateableEntity, ISoftDeleteableEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}

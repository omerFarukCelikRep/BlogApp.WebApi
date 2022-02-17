using BlogApp.Core.Entities.Abstract;
using BlogApp.Core.Entities.Enums;

namespace BlogApp.Core.Entities.Base;

public abstract class BaseEntity : IEntity, ICreatedEntity, IModifiedEntity, IDeletedEntity
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public int AutoId { get; set; }
}

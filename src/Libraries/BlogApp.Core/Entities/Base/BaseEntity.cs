using BlogApp.Core.Entities.Enums;
using BlogApp.Core.Entities.Interfaces;

namespace BlogApp.Core.Entities.Base;

public abstract class BaseEntity : IEntity, ICreatableEntity, IUpdateableEntity
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

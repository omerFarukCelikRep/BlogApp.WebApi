using BlogApp.Core.Entities.Enums;

namespace BlogApp.Core.Entities.Abstract;

public interface IEntity
{
    Guid Id { get; set; }
    Status Status { get; set; }
}
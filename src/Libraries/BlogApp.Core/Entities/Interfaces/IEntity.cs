using BlogApp.Core.Entities.Enums;

namespace BlogApp.Core.Entities.Interfaces;

public interface IEntity
{
    Guid Id { get; set; }
    Status Status { get; set; }
}
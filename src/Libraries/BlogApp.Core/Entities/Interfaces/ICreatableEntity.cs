namespace BlogApp.Core.Entities.Interfaces;

public interface ICreatableEntity
{
    string CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }
}

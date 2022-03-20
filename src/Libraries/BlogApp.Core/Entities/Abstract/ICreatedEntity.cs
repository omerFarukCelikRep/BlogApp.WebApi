namespace BlogApp.Core.Entities.Abstract;

public interface ICreatedEntity
{
    Guid CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }
}

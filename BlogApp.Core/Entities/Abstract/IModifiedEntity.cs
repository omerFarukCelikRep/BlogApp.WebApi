namespace BlogApp.Core.Entities.Abstract;

public interface IModifiedEntity
{
    Guid? ModifiedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
}

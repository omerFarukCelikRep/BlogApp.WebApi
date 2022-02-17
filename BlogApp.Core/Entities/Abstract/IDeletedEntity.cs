namespace BlogApp.Core.Entities.Abstract;

public interface IDeletedEntity
{
    Guid? DeletedBy { get; set; }
    DateTime? DeletedDate { get; set; }
}

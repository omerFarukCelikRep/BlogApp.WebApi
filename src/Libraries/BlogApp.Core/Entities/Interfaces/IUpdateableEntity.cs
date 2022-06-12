namespace BlogApp.Core.Entities.Interfaces;

public interface IUpdateableEntity
{
    string? ModifiedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
}

namespace BlogApp.Core.Entities.Interfaces;

public interface ISoftDeleteableEntity
{
    string? DeletedBy { get; set; }
    DateTime? DeletedDate { get; set; }
}

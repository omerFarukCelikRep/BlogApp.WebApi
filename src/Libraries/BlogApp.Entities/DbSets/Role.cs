namespace BlogApp.Entities.DbSets;
public class Role : AuditableEntity
{
    public string Name { get; set; } = null!;
}
namespace BlogApp.Core.DataAccess.Interfaces;
public interface IRepositoryAsync
{
    Task<int> SaveChangesAsync();
}

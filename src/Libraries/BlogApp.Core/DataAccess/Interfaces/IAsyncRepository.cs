namespace BlogApp.Core.DataAccess.Interfaces;
public interface IAsyncRepository
{
    Task<int> SaveChangesAsync();
}

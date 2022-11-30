namespace BlogApp.Core.DataAccess.Interfaces.Repositories;
public interface IAsyncRepository
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

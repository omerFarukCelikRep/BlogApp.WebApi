using BlogApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlogApp.Core.DataAccess.Interceptors;
public class AuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            AssignBaseProperties(eventData.Context);
        }

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private static void AssignBaseProperties(DbContext context)
    {
        var entries = context.ChangeTracker.Entries<BaseEntity>();
        //var token = context.HttpContext?.Request.Headers["Authorization"]
        //    .FirstOrDefault()?.Split(" ")
        //    .LastOrDefault();
        var userId = "UserNotFound";
        //if (token != null)
        //{
        //    userId = JwtHelper.GetUserIdByToken(token) ?? userId;
        //}

        foreach (var entry in entries)
        {
            SetIfAdded(entry, userId);

            SetIfModified(entry, userId);

            SetIfDeleted(entry, userId);
        }
    }

    private static void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State != EntityState.Deleted)
        {
            return;
        }

        if (entry.Entity is AuditableEntity auditableEntity)
        {
            entry.State = EntityState.Modified;
            entry.Entity.Status = Entities.Enums.Status.Deleted;
            auditableEntity.DeletedDate = DateTime.Now;
            auditableEntity.DeletedBy = userId;
        }
    }

    private static void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Added)
        {
            entry.Entity.CreatedBy = userId;
            entry.Entity.CreatedDate = DateTime.Now;
            entry.Entity.Status = Entities.Enums.Status.Added;
        }
    }

    private static void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Modified)
        {
            entry.Entity.Status = Entities.Enums.Status.Modified;
        }

        entry.Entity.ModifiedBy = userId;
        entry.Entity.ModifiedDate = DateTime.Now;
    }
}

using BlogApp.Core.Entities.Base;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Entities.Configurations;
using BlogApp.Entities.DbSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogApp.DataAccess.Contexts;

public class BlogAppDbContext : DbContext
{
    private readonly IHttpContextAccessor _context;
    public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options, IHttpContextAccessor context) : base(options)
    {
        _context = context;
    }

    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<ArticleTopic> ArticleTopics { get; set; } = null!;
    public DbSet<User> AppUsers { get; set; } = null!;
    public DbSet<UserFollowedTopic> UserFollowedTopics { get; set; } = null!;
    public DbSet<PublishedArticle> PublishedArticles { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(IConfigurationMaker).Assembly);

        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        AssignBaseProperties();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AssignBaseProperties();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AssignBaseProperties()
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var token = _context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
        var userId = "UserNotFound";
        if (token != null)
        {
            userId = JwtHelper.GetUserIdByToken(token) ?? userId;
        }

        foreach (var entry in entries)
        {
            SetIfAdded(entry, userId);

            SetIfModified(entry, userId);

            SetIfDeleted(entry, userId);
        }
    }

    private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State != EntityState.Deleted)
        {
            return;
        }

        if (entry.Entity is AuditableEntity auditableEntity)
        {
            entry.State = EntityState.Modified;
            entry.Entity.Status = Core.Entities.Enums.Status.Deleted;
            auditableEntity.DeletedDate = DateTime.Now;
            auditableEntity.DeletedBy = userId;
        }
    }

    private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Added)
        {
            entry.Entity.CreatedBy = userId;
            entry.Entity.CreatedDate = DateTime.Now;
            entry.Entity.Status = Core.Entities.Enums.Status.Added;
        }
    }

    private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
    {
        if (entry.State == EntityState.Modified)
        {
            entry.Entity.Status = Core.Entities.Enums.Status.Modified;
        }

        entry.Entity.ModifiedBy = userId;
        entry.Entity.ModifiedDate = DateTime.Now;
    }
}

using BlogApp.Core.Entities.Base;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.DataAccess.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogApp.DataAccess.Contexts;

public class BlogAppDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    private readonly IHttpContextAccessor _context;

    public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options, IHttpContextAccessor context) : base(options)
    {
        _context = context;
    }

    public DbSet<Article>? Articles { get; set; }
    public DbSet<ArticleTopic>? ArticleTopics { get; set; }
    public DbSet<Member>? Members { get; set; }
    public DbSet<MemberFollowedTopic>? MemberFollowedTopics { get; set; }
    public DbSet<PublishedArticle>? PublishedArticles { get; set; }
    public DbSet<RefreshToken>? RefreshTokens { get; set; }
    public DbSet<Topic>? Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(IMappingMaker).Assembly);

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

        var token = _context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        var userId = Guid.Empty.ToString();

        if (token != null)
        {
            userId = JwtHelper.GetUserIdByToken(token);
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

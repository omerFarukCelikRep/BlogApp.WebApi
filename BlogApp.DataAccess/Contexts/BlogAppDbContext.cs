using BlogApp.DataAccess.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Contexts;

public class BlogAppDbContext : IdentityDbContext
{
    public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options) : base(options) { }

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
}

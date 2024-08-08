using BlogApp.Entities.Configurations;
using BlogApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DataAccess.Contexts;

public class BlogAppDbContext(DbContextOptions<BlogAppDbContext> options) 
    : DbContext(options)
{
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<ArticleTopic> ArticleTopics { get; set; } = null!;
    public DbSet<User> AppUsers { get; set; } = null!;
    public DbSet<UserFollowedTopic> UserFollowedTopics { get; set; } = null!;
    public DbSet<PublishedArticle> PublishedArticles { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<UserSession> UserSessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(IConfigurationMaker).Assembly);

        base.OnModelCreating(builder);
    }
}
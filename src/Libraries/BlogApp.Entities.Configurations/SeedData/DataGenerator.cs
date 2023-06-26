using BlogApp.Core.Entities.Enums;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Entities.DbSets;
using Bogus;

namespace BlogApp.Entities.Configurations.SeedData;.
public class DataGenerator
{
    private const string DefaultPassword = "newPassword+0";

    private static List<Topic> _topics = TopicGenerator.Generate(20);
    private static List<User> _users = UserGenerator.Generate(20);

    public static IReadOnlyCollection<Topic> Topics => _topics.AsReadOnly();
    public static IReadOnlyCollection<User> Users => _users.AsReadOnly();

#pragma warning disable CS0618 // Type or member is obsolete
    public static Faker<Topic> TopicGenerator => new Faker<Topic>()
         .RuleFor(t => t.Id, _ => Guid.NewGuid())
         .RuleFor(t => t.CreatedBy, _ => Guid.Empty.ToString())
         .RuleFor(t => t.CreatedDate, f => f.Date.Past())
         .RuleFor(t => t.Description, f => f.Lorem.Text())
         .RuleFor(t => t.Status, _ => Status.Added)
         .RuleFor(t => t.Thumbnail, f => f.Image.Abstract())
         .RuleFor(t => t.Name, f => f.Hacker.Adjective());
#pragma warning restore CS0618 // Type or member is obsolete

    public static Faker<ArticleTopic> ArticleTopicGenerator => new Faker<ArticleTopic>()
        .RuleFor(at => at.Id, _ => Guid.NewGuid())
        .RuleFor(at => at.Status, _ => Status.Added)
        .RuleFor(at => at.CreatedBy, _ => Guid.Empty.ToString())
        .RuleFor(at => at.CreatedDate, f => f.Date.Past());


    public static Faker<Article> ArticleGenerator => new Faker<Article>()
        .RuleFor(a => a.Id, _ => Guid.NewGuid())
        .RuleFor(a => a.Title, f => f.Hacker.Phrase())
        .RuleFor(a => a.Title, f => f.Hacker.Phrase())
        .RuleFor(a => a.Content, f => f.Lorem.Paragraphs(5))
        .RuleFor(a => a.ArticleTopics, (f, a) =>
        {
            ArticleTopicGenerator
            .RuleFor(at => at.ArticleId, _ => a.Id)
            .RuleFor(at => at.TopicId, f => f.PickRandom(_topics).Id);

            var articleTopics = ArticleTopicGenerator.GenerateBetween(1, 3);
            foreach (var articleTopic in articleTopics)
            {
                a.ArticleTopics.Add(articleTopic);
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        });

    public static Faker<User> UserGenerator => new Faker<User>()
        .RuleFor(u => u.Id, _ => Guid.NewGuid())
        .RuleFor(u => u.Biography, f => f.Lorem.Paragraph(1))
        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
        .RuleFor(u => u.LastName, f => f.Person.LastName)
        .RuleFor(u => u.CreatedBy, _ => Guid.Empty.ToString())
        .RuleFor(u => u.CreatedDate, f => f.Date.Past())
        .RuleFor(u => u.Status, _ => Status.Added)
        .RuleFor(u => u.Email, f => f.Person.Email)
        .RuleFor(u => new { u.PasswordSalt, u.PasswordHash }, (f, u) =>
        {
            var (salt, hash) = PasswordHelper.HashPassword(DefaultPassword);
            u.PasswordSalt = salt;
            u.PasswordHash = hash;
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        })
        .RuleFor(u => u.Articles, (f, u) =>
        {
            ArticleGenerator
            .RuleFor(a => a.UserId, _ => u.Id);

            var articles = ArticleGenerator.Generate(5);
            foreach (var article in articles)
            {
                u.Articles.Add(article);
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        });
}

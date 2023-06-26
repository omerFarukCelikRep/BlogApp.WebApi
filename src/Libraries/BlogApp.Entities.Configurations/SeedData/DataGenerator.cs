using BlogApp.Core.Entities.Enums;
using BlogApp.Core.Utilities.Authentication;
using Bogus;

namespace BlogApp.Entities.Configurations.SeedData;
public class DataGenerator
{
    private const string DefaultPassword = "newPassword+0";
    private static (byte[], string) DefaultHash = PasswordHelper.HashPassword(DefaultPassword);

    private static List<Topic> _topics = TopicGenerator.Generate(20);
    private static List<User> _users = UserGenerator.Generate(20);
    private static List<Article> _articles = ArticleGenerator.Generate(100);
    private static List<ArticleTopic> _articleTopics = ArticleTopicGenerator.Generate(20);

    public static IReadOnlyCollection<Topic> Topics => _topics.AsReadOnly();
    public static IReadOnlyCollection<User> Users => _users.AsReadOnly();
    public static IReadOnlyCollection<Article> Articles => _articles.AsReadOnly();
    public static IReadOnlyCollection<ArticleTopic> ArticleTopics => _articleTopics.DistinctBy(at => new { at.TopicId, at.ArticleId }).ToList().AsReadOnly();

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
        .RuleFor(at => at.CreatedDate, f => f.Date.Past())
        .RuleFor(at => at.ArticleId, f => f.PickRandom(_articles).Id)
        .RuleFor(at => at.TopicId, f => f.PickRandom(_topics).Id);


    public static Faker<Article> ArticleGenerator => new Faker<Article>()
        .RuleFor(a => a.Id, _ => Guid.NewGuid())
        .RuleFor(a => a.CreatedBy, _ => Guid.Empty.ToString())
        .RuleFor(a => a.CreatedDate, f => f.Date.Past())
        .RuleFor(a => a.Status, _ => Status.Added)
        .RuleFor(a => a.Title, f => f.Hacker.Phrase())
        .RuleFor(a => a.Content, f => f.Lorem.Paragraphs(5))
        .RuleFor(a => a.ReadTime, (f, a) => a.Content.Length / 150)
        .RuleFor(a => a.UserId, f => f.PickRandom(_users).Id);

    public static Faker<User> UserGenerator => new Faker<User>()
        .RuleFor(u => u.Id, _ => Guid.NewGuid())
        .RuleFor(u => u.Biography, f => f.Lorem.Paragraph(1))
        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
        .RuleFor(u => u.LastName, f => f.Person.LastName)
        .RuleFor(u => u.CreatedBy, _ => Guid.Empty.ToString())
        .RuleFor(u => u.CreatedDate, f => f.Date.Past())
        .RuleFor(u => u.Status, _ => Status.Added)
        .RuleFor(u => u.Email, f => f.Person.Email)
        .RuleFor(u => u.PasswordSalt, _ => DefaultHash.Item1)
        .RuleFor(u => u.PasswordHash, _ => DefaultHash.Item2);
}

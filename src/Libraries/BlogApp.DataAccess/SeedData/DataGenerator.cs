using BlogApp.Core.Entities.Enums;
using BlogApp.Core.Utilities.Authentication;
using BlogApp.Entities.DbSets;
using Bogus;

namespace BlogApp.DataAccess.SeedData;
public class DataGenerator
{
    private const string DefaultPassword = "newPassword+0";

    public static Faker<Topic> TopicGenerator => new Faker<Topic>()
         .RuleFor(t => t.Id, _ => Guid.NewGuid())
         .RuleFor(t => t.CreatedBy, _ => Guid.Empty.ToString())
         .RuleFor(t => t.CreatedDate, f => f.Date.Past())
         .RuleFor(t => t.Description, f => f.Lorem.Text())
         .RuleFor(t => t.Status, _ => Status.Added)
         .RuleFor(t => t.Name, f => f.Hacker.Adjective());

    public static Faker<Article> ArticleGenerator => new Faker<Article>()
        .RuleFor(a => a.Id, _ => Guid.NewGuid())
        .RuleFor(a => a.Title, f => f.Hacker.Phrase())
        .RuleFor(a => a.Title, f => f.Hacker.Phrase())
        .RuleFor(a => a.Content, f => f.Lorem.Paragraphs(5));

    public static Faker<User> UserGenerator => new Faker<User>()
        .RuleFor(u => u.Id, _ => Guid.NewGuid())
        .RuleFor(u => u.Biography, f => f.Lorem.Paragraph(1))
        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
        .RuleFor(u => u.LastName, f => f.Person.LastName)
        .RuleFor(u => u.CreatedBy, _ => Guid.Empty.ToString())
        .RuleFor(u => u.CreatedDate, f => f.Date.Past())
        .RuleFor(u => u.Status, _ => Status.Added)
        .RuleFor(u => u.Email, f => f.Person.Email)
        .RuleFor(u => u.MemberFollowedTopics, (f, u) =>
        {

            return null;
        })
        .RuleFor(u => new { u.PasswordSalt, u.PasswordHash }, (f, u) =>
        {
            var (salt, hash) = PasswordHelper.HashPassword(DefaultPassword);
            u.PasswordSalt = salt;
            u.PasswordHash = hash;
            return new { u.PasswordSalt, u.PasswordHash };
        })
        .RuleFor(u => u.Articles, (f, u) =>
        {
            ArticleGenerator
            .RuleFor(a => a.UserId, _ => u.Id);

            var articles = ArticleGenerator.Generate(10);

            return null;
        });
}

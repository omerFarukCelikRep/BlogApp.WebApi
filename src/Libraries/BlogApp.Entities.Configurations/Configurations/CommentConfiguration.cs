﻿namespace BlogApp.Entities.Configurations.Configurations;
public class CommentConfiguration : AuditableEntityConfiguration<Comment>
{
    private const string TableName = "Comments";

    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName);

        builder.Property(x => x.UserName)
               .HasMaxLength(512)
               .IsRequired();
        builder.Property(x => x.Text)
               .IsRequired();
        builder.Property(x => x.UserIpAdress)
               .IsRequired();
        builder.Property(x => x.ArticleId)
               .IsRequired();
        builder.Property(x => x.UserId)
               .IsRequired(false);

        builder.HasOne(x => x.Article)
               .WithMany(x => x.Comments)
               .HasForeignKey(x => x.ArticleId);
    }
}
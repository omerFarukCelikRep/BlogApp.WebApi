﻿using BlogApp.Core.DataAccess.Base.EntityFramework.Mapping;
using BlogApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Mapping;
public class MemberMap : BaseMap<Member>
{
    public override void Configure(EntityTypeBuilder<Member> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Biography).IsRequired(false);
        builder.Property(x => x.ProfilePicture).IsRequired(false);
        builder.Property(x => x.Url).IsRequired(false);
        builder.Property(x => x.IdentityId).IsRequired();
    }
}

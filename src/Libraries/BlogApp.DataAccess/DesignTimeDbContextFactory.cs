﻿using BlogApp.Core.Utilities.Configurations;
using BlogApp.Core.Utilities.Constants;
using BlogApp.DataAccess.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogApp.DataAccess;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BlogAppDbContext>
{
    public BlogAppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<BlogAppDbContext> optionsBuilder = new();

        optionsBuilder.UseSqlServer(Configuration.GetConnectionString(DatabaseConstants.DefaultConnectionString));

        return new(optionsBuilder.Options, new HttpContextAccessor());
    }
}

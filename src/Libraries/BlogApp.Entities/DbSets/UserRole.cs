﻿namespace BlogApp.Entities.DbSets;
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

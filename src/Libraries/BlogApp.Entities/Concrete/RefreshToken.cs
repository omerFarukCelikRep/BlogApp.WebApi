using BlogApp.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entities.Concrete;

public class RefreshToken : BaseEntity
{
    public string? Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string? CreatedByIp { get; set; }
    public bool IsExpired
    {
        get
        {
            return DateTime.Now >= ExpiryDate;
        }
    }
    public bool IsRevoked
    {
        get
        {
            return RevokedDate != null;
        }
    }
    public bool IsActive
    {
        get
        {
            return RevokedDate == null && !IsExpired;
        }
    }

    //Navigation Prop.
    public Guid UserId { get; set; }
    public virtual IdentityUser<Guid>? User { get; set; }
}

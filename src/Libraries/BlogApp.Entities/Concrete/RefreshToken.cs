using BlogApp.Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Entities.Concrete;
public class RefreshToken : BaseEntity
{
    public string? Token { get; set; }
    public string? JwtId { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime ExpiryDate { get; set; }

    //Navigation Prop.
    public Guid? UserId { get; set; }
    public virtual IdentityUser<Guid>? User { get; set; }
}

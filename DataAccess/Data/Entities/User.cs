using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser, BaseEntity
    {
        public DateTime? Birthdate { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}

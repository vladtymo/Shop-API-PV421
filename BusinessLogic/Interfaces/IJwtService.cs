using DataAccess.Data.Entities;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        // ------- Access Token
        IEnumerable<Claim> GetClaims(User user);
        string GenerateToken(IEnumerable<Claim> claims);

        // ------- Refresh Token
        //string GenerateRefreshToken();
        //IEnumerable<Claim> GetClaimsFromExpiredToken(string token);
        //DateTime GetLastValidRefreshTokenDate();
    }
}

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
        RefreshToken GenerateRefreshToken(string ipAddress);
        //IEnumerable<Claim> GetClaimsFromExpiredToken(string token);
        //DateTime GetLastValidRefreshTokenDate();
    }
}

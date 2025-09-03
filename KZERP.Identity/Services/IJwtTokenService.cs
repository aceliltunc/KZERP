using System.Security.Claims;

namespace KZERP.Identity.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
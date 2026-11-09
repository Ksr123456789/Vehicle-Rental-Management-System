using System.Security.Claims;

namespace RentalManagementSystem.Application.ServiceContracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken); 
    }
}

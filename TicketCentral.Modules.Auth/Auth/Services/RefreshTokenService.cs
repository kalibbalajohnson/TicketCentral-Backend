using System.Security.Cryptography;

namespace TicketCentral.Modules.Auth.Services;

public class RefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
}
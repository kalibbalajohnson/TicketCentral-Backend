namespace TicketCentral.Modules.Auth.DTOs;

public class AuthResponseDTO
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;
    
    public string RefreshToken { get; set; } = string.Empty;

    public DateTime? LastLoginAt { get; set; }

    public bool IsVerified { get; set; }

    public bool IsLoggedIn { get; set; }
}
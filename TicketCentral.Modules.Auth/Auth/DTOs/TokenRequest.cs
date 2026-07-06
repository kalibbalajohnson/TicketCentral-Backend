namespace TicketCentral.Modules.Auth.DTOs;

public class TokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}
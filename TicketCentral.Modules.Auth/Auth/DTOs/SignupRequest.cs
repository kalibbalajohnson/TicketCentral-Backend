using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Modules.Auth.DTOs;

public class SignUpDTO
{
    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

}
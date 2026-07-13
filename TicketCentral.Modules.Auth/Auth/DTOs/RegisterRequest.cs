using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Modules.Auth.DTOs;

public class RegisterRequestDTO
{
    [Required, MaxLength(150)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Phone { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;
using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Modules.Auth.DTOs;

public class UpdateUserDetailsDto
{
    [MaxLength(100)]
    public string? FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? LastName { get; set; } = string.Empty;

    [StringLength(10)]
    public UserGender? Gender { get; set; }

    [StringLength(100)]
    public string? IDPassportNumber { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; } = string.Empty;
}
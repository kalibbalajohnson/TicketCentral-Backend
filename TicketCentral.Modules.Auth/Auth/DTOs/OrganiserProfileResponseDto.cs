namespace TicketCentral.Modules.Auth.DTOs;

public class OrganiserProfileResponseDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string ProfileName { get; set; } = string.Empty;

    public string? ProfileEmail { get; set; }

    public string? Description { get; set; }

    public string? ProfileImage { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? BankName { get; set; }

    public string? BankAccountName { get; set; }

    public string? BankAccountNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }
}
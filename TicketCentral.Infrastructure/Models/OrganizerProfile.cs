using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketCentral.Infrastructure.Models;

public class OrganiserProfile
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // Foreign Key
    [Required]
    public Guid UserId { get; set; }

    // Navigation Property
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    [StringLength(150)]
    public string ProfileName { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(255)]
    public string? ProfileEmail { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? ProfileImage { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? BankName { get; set; }

    [StringLength(100)]
    public string? BankAccountName { get; set; }

    [StringLength(50)]
    public string? BankAccountNumber { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? EditedAt { get; set; }
}
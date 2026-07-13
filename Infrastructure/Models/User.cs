using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Infrastructure.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; } = UserRole.User;

    [Required]
    public bool IsLoggedIn { get; set; } = false;

    [Required]
    public bool IsVerified { get; set; } = false;

    public DateTime? LastLoginAt { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? EditedAt { get; set; }

    [StringLength(500)]
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }
}

public enum UserRole
    {
        Admin,
        User,
        Organiser
    }
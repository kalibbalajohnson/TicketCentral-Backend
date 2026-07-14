using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Infrastructure.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();


    [StringLength(100)]
    public string? FirstName { get; set; } = string.Empty;


    [StringLength(100)]
    public string? LastName { get; set; } = string.Empty;

    [StringLength(10)]
    public UserGender? Gender { get; set; }


    [StringLength(100)]
    public string? IDPassportNumber { get; set; } = string.Empty;


    public DateTime? DateOfBirth { get; set; }


    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;


    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;


    [StringLength(20)]
    public string? PhoneNumber { get; set; } = string.Empty;


    [Required]
    public UserRole Role { get; set; } = UserRole.User;


    public bool IsLoggedIn { get; set; } = false;


    public bool IsVerified { get; set; } = false;


    public DateTime? LastLoginAt { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public DateTime? EditedAt { get; set; }


    [StringLength(500)]
    public string? RefreshToken { get; set; }


    public DateTime? RefreshTokenExpiryTime { get; set; }


    // One-to-One Navigation Property
    public OrganiserProfile? OrganiserProfile { get; set; }


    // One-to-Many Navigation Property
    public ICollection<Event> Events { get; set; } = new List<Event>();
}


public enum UserRole
{
    Admin,
    User
}

public enum UserGender
{
    Male,
    Female
}
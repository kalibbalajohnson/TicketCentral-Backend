using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketCentral.Infrastructure.Models;

public class TicketOrder
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();


    // ---------------- USER (OPTIONAL) ----------------

    public Guid? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }


    // ---------------- EVENT ----------------

    [Required]
    public Guid EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;


    // ---------------- TICKET TYPE ----------------

    [Required]
    public Guid TicketTypeId { get; set; }

    [ForeignKey(nameof(TicketTypeId))]
    public TicketType TicketType { get; set; } = null!;

    public int Quantity { get; set; } = 1;


    // ---------------- CUSTOMER DETAILS ----------------

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;


    // ---------------- PAYMENT ----------------

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

    [Required]
    public bool UserPaysBookingFee { get; set; }

    [Required]
    public bool IsPaid { get; set; } = false;


    // ---------------- QR CODE ----------------

    [Required]
    [StringLength(500)]
    public string QRCode { get; set; } = string.Empty;

    [Required]
    public bool IsScanned { get; set; } = false;


    // ---------------- REVENUE ----------------

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal OrganiserDividend { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PlatformDividend { get; set; }


    // ---------------- TRANSACTION ----------------
    // generated reference sent to Relworx
    [Required]
    [StringLength(36)]
    public string PaymentReference { get; set; } = string.Empty;


    // Relworx returned internal reference
    [StringLength(100)]
    public string? ProviderReference { get; set; }


    // ---------------- AUDIT ----------------

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? EditedAt { get; set; }
}


public enum PaymentMethod
{
    MTNMomo,
    AirtelMoney,
    Card
}
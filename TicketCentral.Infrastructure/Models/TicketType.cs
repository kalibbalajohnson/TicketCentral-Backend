using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketCentral.Infrastructure.Models;

public class TicketType
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Required]
    public Guid EventId { get; set; }


    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;


    [Required]
    public TicketCategory Category { get; set; }


    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;


    [StringLength(500)]
    public string? Description { get; set; }


    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }


    // Total tickets created
    [Required]
    [Range(1, int.MaxValue)]
    public int TotalNumber { get; set; }


    // Remaining tickets
    [Required]
    [Range(0, int.MaxValue)]
    public int TotalNumberLeft { get; set; }


    [Required]
    public DateTime SaleStartDateTime { get; set; }


    [Required]
    public DateTime SaleEndDateTime { get; set; }


    [Required]
    public bool ShowTicketStock { get; set; } = false;


    [Required]
    [StringLength(10)]
    public string Currency { get; set; } = "UGX";


    [Required]
    public bool IsActive { get; set; } = true;


    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public DateTime? EditedAt { get; set; }

    public ICollection<TicketOrder> TicketOrders { get; set; } = new List<TicketOrder>();
}



public enum TicketCategory
{
    Free,
    Paid
}
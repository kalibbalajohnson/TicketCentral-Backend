using System.ComponentModel.DataAnnotations;
using TicketCentral.Infrastructure.Models;
namespace TicketCentral.Modules.Events.Tickets.DTOs;

public class CreateTicketTypeDto
{
    [Required]
    public Guid EventId { get; set; }


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


    [Required]
    [Range(1, int.MaxValue)]
    public int TotalNumber { get; set; }


    [Required]
    public DateTime SaleStartDateTime { get; set; }


    [Required]
    public DateTime SaleEndDateTime { get; set; }


    public bool ShowTicketStock { get; set; } = false;


    [Required]
    [StringLength(10)]
    public string Currency { get; set; } = "UGX";
}
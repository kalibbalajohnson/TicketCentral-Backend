using System.ComponentModel.DataAnnotations;
using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Modules.Events.Events.DTOs;

public class UpdateEventDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;


    [StringLength(255)]
    public string? Description { get; set; }


    [StringLength(500)]
    public string? Image { get; set; }


    [Required]
    public EventType Type { get; set; }


    [Required]
    public EventCategory Category { get; set; }


    [Required]
    [StringLength(100)]
    public string Venue { get; set; } = string.Empty;


    [StringLength(500)]
    public string? EventUrl { get; set; }


    [Required]
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }


    [Required]
    public DateTime EventDate { get; set; }


    public bool IsFeatured { get; set; }
}
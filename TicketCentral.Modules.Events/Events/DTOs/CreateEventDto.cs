using System.ComponentModel.DataAnnotations;
using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Modules.Events.Events.DTOs;

public class CreateEventDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? ListingImage { get; set; }

    [StringLength(500)]
    public string? BannerImage { get; set; }

    [Required]
    public EventType Type { get; set; }

    [Required]
    public EventCategory Category { get; set; }

    [Required]
    [StringLength(100)]
    public string Venue { get; set; } = string.Empty;

    [StringLength(500)]
    public string? EventUrl { get; set; }

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [Required]
    [StringLength(50)]
    public EventOccurrence? Occurrence { get; set; } = EventOccurrence.Single;

    [Required]
    public DateTime EventStartDateTime { get; set; }

    [Required]
    public DateTime EventEndDateTime { get; set; }

    public bool IsFeatured { get; set; } = false;

    public bool IsPrivate { get; set; } = false;
}
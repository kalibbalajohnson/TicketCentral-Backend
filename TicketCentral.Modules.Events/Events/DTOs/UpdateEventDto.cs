using System.ComponentModel.DataAnnotations;
using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Modules.Events.Events.DTOs;

public class UpdateEventDto
{
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? ListingImage { get; set; }

    [StringLength(500)]
    public string? BannerImage { get; set; }

    public EventType Type { get; set; }

    public EventCategory Category { get; set; }

    [StringLength(100)]
    public string Venue { get; set; } = string.Empty;

    [StringLength(500)]
    public string? EventUrl { get; set; }

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [StringLength(50)]
    public EventOccurrence? Occurrence { get; set; } = EventOccurrence.Single;

    public DateTime EventStartDateTime { get; set; }

    public DateTime EventEndDateTime { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsPrivate { get; set; }
}
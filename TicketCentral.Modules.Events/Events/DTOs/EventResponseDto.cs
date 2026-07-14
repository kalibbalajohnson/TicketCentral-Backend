using TicketCentral.Infrastructure.Models;
namespace TicketCentral.Modules.Events.Events.DTOs;

public class EventResponseDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ListingImage { get; set; }

    public string? BannerImage { get; set; }

    public EventType Type { get; set; }

    public EventCategory Category { get; set; }

    public string Venue { get; set; } = string.Empty;

    public string? EventUrl { get; set; }

    public int Capacity { get; set; }

    public EventOccurrence? Occurrence { get; set; }

    public DateTime EventStartDateTime { get; set; }

    public DateTime EventEndDateTime { get; set; }

    public EventStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public string? Slug { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsPrivate { get; set; }
}
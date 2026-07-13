using TicketCentral.Infrastructure.Models;
namespace TicketCentral.Modules.Events.Events.DTOs;

public class EventResponseDto
{
    public Guid Id { get; set; }

    public Guid OrganiserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public EventType Type { get; set; }

    public EventCategory Category { get; set; }

    public string Venue { get; set; } = string.Empty;

    public string? EventUrl { get; set; }

    public int Capacity { get; set; }

    public DateTime EventDate { get; set; }

    public EventStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public string? Slug { get; set; }

    public bool IsFeatured { get; set; }
}
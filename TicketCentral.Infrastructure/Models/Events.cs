using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

namespace TicketCentral.Infrastructure.Models;

public class Event
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // Foreign Key
    [Required]
    public Guid UserId { get; set; }

    // Navigation Property
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;


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


    [Required]
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    [Required]
    [StringLength(50)]
    public EventOccurrence? Occurrence { get; set; } = EventOccurrence.Single;

    [Required]
    public DateTime EventStartDateTime { get; set; }

    [Required]
    public DateTime EventEndDateTime { get; set; }

    [Required]
    public EventStatus Status { get; set; } = EventStatus.Draft;


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public DateTime? EditedAt { get; set; }


    [StringLength(250)]
    public string? Slug { get; set; }


    public bool IsFeatured { get; set; } = false;

    public bool IsPrivate { get; set; } = false;
}



public enum EventType
{
    Physical,
    Virtual
}


public enum EventOccurrence
{
    Single,
    Recurring
}


public enum EventStatus
{
    Draft,
    Published,
    Cancelled,
    Completed
}


public enum EventCategory
{
    Music,
    Sports,
    Conference,
    Seminar,
    Workshop,
    Festival,
    Theatre,
    Exhibition,
    Religious,
    Charity,
    Education,
    Business,
    Networking,
    Entertainment,
    Other
}
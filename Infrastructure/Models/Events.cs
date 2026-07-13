using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Infrastructure.Models;

    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid OrganiserId { get; set; }

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

        [Required]
        public EventStatus Status { get; set; } = EventStatus.Published;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? EditedAt { get; set; }

        [StringLength(250)]
        public string? Slug { get; set; }

        public bool IsFeatured { get; set; } = false;

    }

    public enum EventType
    {
        Physical,
        Virtual
    }

    public enum EventStatus
    {
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
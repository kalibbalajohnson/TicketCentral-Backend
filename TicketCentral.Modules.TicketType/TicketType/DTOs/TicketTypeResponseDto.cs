namespace TicketCentral.Modules.Events.Tickets.DTOs;
using TicketCentral.Infrastructure.Models;

public class TicketTypeResponseDto
{
    public Guid Id { get; set; }


    public Guid EventId { get; set; }


    public TicketCategory Category { get; set; }


    public string Title { get; set; } = string.Empty;


    public string? Description { get; set; }


    public decimal Price { get; set; }


    public int TotalNumber { get; set; }


    public int TotalNumberLeft { get; set; }


    public DateTime SaleStartDateTime { get; set; }


    public DateTime SaleEndDateTime { get; set; }


    public bool ShowTicketStock { get; set; }


    public string Currency { get; set; } = string.Empty;


    public bool IsActive { get; set; }


    public DateTime CreatedAt { get; set; }


    public DateTime? EditedAt { get; set; }
}
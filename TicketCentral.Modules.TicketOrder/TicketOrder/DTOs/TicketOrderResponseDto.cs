using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Modules.TicketOrders.DTOs;

public class TicketOrderResponseDto
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid EventId { get; set; }

    public Guid TicketTypeId { get; set; }

    public int Quantity { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public PaymentMethod PaymentMethod { get; set; }

    public string QRCode { get; set; } = string.Empty;

    public bool IsScanned { get; set; }

    public bool UserPaysBookingFee { get; set; }

    public bool IsPaid { get; set; }

    public decimal OrganiserDividend { get; set; }

    public decimal PlatformDividend { get; set; }

    public string PaymentReference { get; set; } = string.Empty;

    public string ProviderReference { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Modules.TicketOrders.DTOs;

public class PaymentCallbackDto
{

    [Required]
    public Guid TicketOrderId { get; set; }

    [Required]
    public string TransactionReference { get; set; } = string.Empty;

    [Required]
    public string ProviderTransactionId { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "UGX";

    public string? Message { get; set; }

    public DateTime PaidAt { get; set; }
}
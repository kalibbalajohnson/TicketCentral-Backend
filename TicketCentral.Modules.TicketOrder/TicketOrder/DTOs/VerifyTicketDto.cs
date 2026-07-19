using System.ComponentModel.DataAnnotations;

namespace TicketCentral.Modules.TicketOrders.DTOs;

public class VerifyTicketDto
{
    [Required]
    public string QRCode { get; set; } = string.Empty;
}
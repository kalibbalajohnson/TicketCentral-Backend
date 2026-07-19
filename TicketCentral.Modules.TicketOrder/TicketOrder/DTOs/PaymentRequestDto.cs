using System.ComponentModel.DataAnnotations;
using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Modules.TicketOrders.DTOs;

public class PaymentRequestDto
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

    [Required]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}
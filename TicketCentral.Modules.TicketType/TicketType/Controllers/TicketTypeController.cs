using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketCentral.Infrastructure.Data;
using TicketCentral.Infrastructure.Models;
using TicketCentral.Modules.Events.Tickets.DTOs;

namespace TicketCentral.Modules.Events.Tickets.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class TicketTypesController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<TicketTypesController> _logger;


    public TicketTypesController(
        AppDbContext dbContext,
        ILogger<TicketTypesController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }



    // ---------------- GET TICKET TYPES BY EVENT ----------------
    [HttpGet("event/{eventId:guid}")]
    public async Task<IActionResult> GetTicketTypes(Guid eventId)
    {
        try
        {
            var tickets = await _dbContext.TicketTypes
                .Where(x => x.EventId == eventId &&
                            x.IsActive)
                .Select(x => new TicketTypeResponseDto
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    Category = x.Category,
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    TotalNumber = x.TotalNumber,
                    TotalNumberLeft = x.TotalNumberLeft,
                    SaleStartDateTime = x.SaleStartDateTime,
                    SaleEndDateTime = x.SaleEndDateTime,
                    ShowTicketStock = x.ShowTicketStock,
                    Currency = x.Currency,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    EditedAt = x.EditedAt
                })
                .ToListAsync();


            return Ok(tickets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error fetching ticket types");

            return StatusCode(500,
                "Internal server error");
        }
    }




    // ---------------- GET SINGLE TICKET TYPE ----------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTicketType(Guid id)
    {
        try
        {
            var ticket =
                await _dbContext.TicketTypes
                .FirstOrDefaultAsync(x => x.Id == id);


            if (ticket == null)
                return NotFound("Ticket type not found");


            return Ok(MapResponse(ticket));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error fetching ticket type");

            return StatusCode(500,
                "Internal server error");
        }
    }




    // ---------------- CREATE TICKET TYPE ----------------
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTicketType(
        CreateTicketTypeDto request)
    {
        try
        {
            var ticket = new TicketType
            {
                Id = Guid.NewGuid(),

                EventId = request.EventId,

                Category = request.Category,

                Title = request.Title,

                Description = request.Description,

                Price = request.Price,

                TotalNumber = request.TotalNumber,

                TotalNumberLeft = request.TotalNumber,

                SaleStartDateTime =
                    request.SaleStartDateTime,

                SaleEndDateTime =
                    request.SaleEndDateTime,

                ShowTicketStock =
                    request.ShowTicketStock,

                Currency =
                    request.Currency,

                IsActive = true,

                CreatedAt = DateTime.UtcNow
            };


            _dbContext.TicketTypes.Add(ticket);

            await _dbContext.SaveChangesAsync();


            return CreatedAtAction(
                nameof(GetTicketType),
                new { id = ticket.Id },
                MapResponse(ticket));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating ticket type");

            return StatusCode(500,
                "Internal server error");
        }
    }




    // ---------------- UPDATE TICKET TYPE ----------------
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTicketType(
        Guid id,
        UpdateTicketTypeDto request)
    {
        try
        {
            var ticket =
                await _dbContext.TicketTypes
                .FindAsync(id);


            if (ticket == null)
                return NotFound("Ticket type not found");


            ticket.Category = request.Category;

            ticket.Title = request.Title;

            ticket.Description = request.Description;

            ticket.Price = request.Price;

            ticket.TotalNumber = request.TotalNumber;

            ticket.SaleStartDateTime =
                request.SaleStartDateTime;

            ticket.SaleEndDateTime =
                request.SaleEndDateTime;

            ticket.ShowTicketStock =
                request.ShowTicketStock;

            ticket.Currency =
                request.Currency;

            ticket.IsActive =
                request.IsActive;

            ticket.EditedAt =
                DateTime.UtcNow;



            await _dbContext.SaveChangesAsync();


            return Ok(MapResponse(ticket));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error updating ticket type");

            return StatusCode(500,
                "Internal server error");
        }
    }




    // ---------------- DELETE TICKET TYPE ----------------
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTicketType(Guid id)
    {
        try
        {
            var ticket =
                await _dbContext.TicketTypes
                .FindAsync(id);


            if (ticket == null)
                return NotFound("Ticket type not found");


            _dbContext.TicketTypes.Remove(ticket);

            await _dbContext.SaveChangesAsync();


            return Ok(
                "Ticket type deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error deleting ticket type");

            return StatusCode(500,
                "Internal server error");
        }
    }




    // ---------------- ACTIVATE / DEACTIVATE ----------------
    [Authorize]
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        [FromBody] bool isActive)
    {
        try
        {
            var ticket =
                await _dbContext.TicketTypes
                .FindAsync(id);


            if (ticket == null)
                return NotFound("Ticket type not found");


            ticket.IsActive = isActive;

            ticket.EditedAt =
                DateTime.UtcNow;


            await _dbContext.SaveChangesAsync();


            return Ok(new
            {
                message =
                "Ticket status updated successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error updating ticket status");

            return StatusCode(500,
                "Internal server error");
        }
    }




    private static TicketTypeResponseDto MapResponse(
        TicketType ticket)
    {
        return new TicketTypeResponseDto
        {
            Id = ticket.Id,

            EventId = ticket.EventId,

            Category = ticket.Category,

            Title = ticket.Title,

            Description = ticket.Description,

            Price = ticket.Price,

            TotalNumber = ticket.TotalNumber,

            TotalNumberLeft = ticket.TotalNumberLeft,

            SaleStartDateTime =
                ticket.SaleStartDateTime,

            SaleEndDateTime =
                ticket.SaleEndDateTime,

            ShowTicketStock =
                ticket.ShowTicketStock,

            Currency =
                ticket.Currency,

            IsActive =
                ticket.IsActive,

            CreatedAt =
                ticket.CreatedAt,

            EditedAt =
                ticket.EditedAt
        };
    }
}
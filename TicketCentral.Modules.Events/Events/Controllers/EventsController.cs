using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketCentral.Infrastructure.Data;
using TicketCentral.Infrastructure.Models;

using TicketCentral.Modules.Events.Events.DTOs;

namespace TicketCentral.Modules.Events.Events.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<EventsController> _logger;


    public EventsController(
        AppDbContext dbContext,
        ILogger<EventsController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }



    // ---------------- GET ALL EVENTS ----------------
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        try
        {
            _logger.LogInformation("Fetching all events");


            var events = await _dbContext.Events
                .Where(x => x.Status == EventStatus.Published)
                .OrderByDescending(x => x.EventDate)
                .Select(x => new EventResponseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Category = x.Category,
                    Type = x.Type,
                    Venue = x.Venue,
                    EventDate = x.EventDate,
                    Capacity = x.Capacity,
                    Status = x.Status,
                    IsFeatured = x.IsFeatured,
                    Slug = x.Slug
                })
                .ToListAsync();


            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching events");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- SEARCH EVENTS ----------------
    [HttpGet("search")]
    public async Task<IActionResult> SearchEvents(
        [FromQuery] string query)
    {
        try
        {
            _logger.LogInformation("Searching events {Query}", query);


            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search query is required");


            query = query.ToLower();


            var events = await _dbContext.Events
                .Where(x =>
                    x.Status == EventStatus.Published &&
                    (
                        x.Title.ToLower().Contains(query) ||
                        x.Venue.ToLower().Contains(query) ||
                        x.Description!.ToLower().Contains(query)
                    ))
                .Select(x => new EventResponseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Category = x.Category,
                    Type = x.Type,
                    Venue = x.Venue,
                    EventDate = x.EventDate,
                    Capacity = x.Capacity,
                    Status = x.Status,
                    IsFeatured = x.IsFeatured,
                    Slug = x.Slug
                })
                .ToListAsync();


            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching events");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- GET EVENT BY ID ----------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        try
        {
            var eventItem = await _dbContext.Events
                .FirstOrDefaultAsync(x => x.Id == id);


            if (eventItem == null)
                return NotFound("Event not found");


            return Ok(MapResponse(eventItem));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching event {Id}", id);

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- GET EVENT BY SLUG ----------------
    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetEventBySlug(string slug)
    {
        try
        {
            var eventItem = await _dbContext.Events
                .FirstOrDefaultAsync(x => x.Slug == slug);


            if (eventItem == null)
                return NotFound("Event not found");


            return Ok(MapResponse(eventItem));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching event by slug");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- GET FEATURED EVENTS ----------------
    [HttpGet("featured")]
    public async Task<IActionResult> GetFeaturedEvents()
    {
        try
        {
            var events = await _dbContext.Events
                .Where(x =>
                    x.IsFeatured &&
                    x.Status == EventStatus.Published)
                .Select(x => new EventResponseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                    Category = x.Category,
                    Type = x.Type,
                    Venue = x.Venue,
                    EventDate = x.EventDate,
                    Capacity = x.Capacity,
                    Status = x.Status,
                    IsFeatured = x.IsFeatured,
                    Slug = x.Slug
                })
                .ToListAsync();


            return Ok(events);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching featured events");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- CREATE EVENT ----------------
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateEvent(
        CreateEventDto request)
    {
        try
        {
            _logger.LogInformation(
                "Creating event {Title}",
                request.Title);


            var eventItem = new Event
            {
                Id = Guid.NewGuid(),
                OrganiserId = request.OrganiserId,
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
                Type = request.Type,
                Category = request.Category,
                Venue = request.Venue,
                EventUrl = request.EventUrl,
                Capacity = request.Capacity,
                EventDate = request.EventDate,
                Status = EventStatus.Published,
                Slug = request.Title.ToLower()
                    .Replace(" ", "-"),
                IsFeatured = request.IsFeatured,
                CreatedAt = DateTime.UtcNow
            };


            _dbContext.Events.Add(eventItem);

            await _dbContext.SaveChangesAsync();


            return CreatedAtAction(
                nameof(GetEvent),
                new { id = eventItem.Id },
                MapResponse(eventItem));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating event");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- UPDATE EVENT ----------------
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEvent(
        Guid id,
        UpdateEventDto request)
    {
        try
        {
            var eventItem =
                await _dbContext.Events.FindAsync(id);


            if (eventItem == null)
                return NotFound("Event not found");


            eventItem.Title = request.Title;
            eventItem.Description = request.Description;
            eventItem.Image = request.Image;
            eventItem.Type = request.Type;
            eventItem.Category = request.Category;
            eventItem.Venue = request.Venue;
            eventItem.EventUrl = request.EventUrl;
            eventItem.Capacity = request.Capacity;
            eventItem.EventDate = request.EventDate;
            eventItem.IsFeatured = request.IsFeatured;
            eventItem.EditedAt = DateTime.UtcNow;


            await _dbContext.SaveChangesAsync();


            return Ok(MapResponse(eventItem));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating event");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- DELETE EVENT ----------------
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        try
        {
            var eventItem =
                await _dbContext.Events.FindAsync(id);


            if (eventItem == null)
                return NotFound("Event not found");


            _dbContext.Events.Remove(eventItem);

            await _dbContext.SaveChangesAsync();


            return Ok("Event deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting event");

            return StatusCode(500, "Internal server error");
        }
    }



    // ---------------- UPDATE EVENT STATUS ----------------
    [Authorize]
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        EventStatus status)
    {
        try
        {
            var eventItem =
                await _dbContext.Events.FindAsync(id);


            if (eventItem == null)
                return NotFound("Event not found");


            eventItem.Status = status;
            eventItem.EditedAt = DateTime.UtcNow;


            await _dbContext.SaveChangesAsync();


            return Ok("Event status updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating event status");

            return StatusCode(500, "Internal server error");
        }
    }



    private static EventResponseDto MapResponse(Event e)
    {
        return new EventResponseDto
        {
            Id = e.Id,
            OrganiserId = e.OrganiserId,
            Title = e.Title,
            Description = e.Description,
            Image = e.Image,
            Type = e.Type,
            Category = e.Category,
            Venue = e.Venue,
            EventUrl = e.EventUrl,
            Capacity = e.Capacity,
            EventDate = e.EventDate,
            Status = e.Status,
            CreatedAt = e.CreatedAt,
            EditedAt = e.EditedAt,
            Slug = e.Slug,
            IsFeatured = e.IsFeatured
        };
    }
}
using HolyCRMApi.Dtos;
using HolyCRMApi.Dtos.Events;
using HolyCRMApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolyCRMApi.Controllers;

/// <summary>
/// Events are happenings that take place in your church or are connected to it
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class EventsController(IEventService eventService, ILogger<EventsController> logger) : ControllerBase
{
    /// <summary>
    /// Get all events
    /// </summary>
    /// <param name="query"></param>
    /// <returns>List of Events</returns>
    [HttpGet(Name = "GetEvents")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<EventDto>>> GetEvents([FromQuery] GetEventsQuery query)
    {
        logger.LogDebug("Fetching events on Page={Page} with PageSize={PageSize}", query.Page, query.PageSize);
        
        var events = await eventService.GetAllAsync(query.Page, query.PageSize);
        
        logger.LogDebug("Returned {Count} events", events.Count);

        return Ok(events);
    }

    [HttpGet("{eventId}", Name = "GetEventById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventDto>> GetEventById([FromRoute] Guid eventId)
    {
        logger.LogDebug("Fetching Event with EventId={eventId}", eventId);

        var singleEvent = await eventService.GetByIdAsync(eventId);

        if (singleEvent is null)
        {
            logger.LogWarning("Event not found with EventId={eventId}", eventId);
            return NotFound();
        }

        return Ok(singleEvent);
    }

    [HttpPost(Name = "CreateEvent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EventDto>> CreateEvent([FromBody] CreateEventRequest request)
    {
        logger.LogDebug("Creating event");
        var newEvent = await eventService.CreateAsync(request);
        return CreatedAtAction(nameof(GetEventById), new { eventId = newEvent.Id }, newEvent);
    }
    
}

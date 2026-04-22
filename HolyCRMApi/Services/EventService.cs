using HolyCRMApi.Data;
using HolyCRMApi.Dtos;
using HolyCRMApi.Dtos.Events;
using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Services;

/// <summary>
/// Handles data access and business logic for church events.
/// </summary>
/// <param name="db">The database context.</param>
/// <param name="logger">The logger.</param>
public class EventService(AppDbContext db, ILogger<EventService> logger) : IEventService
{
    /// <summary>
    /// Returns a paginated list of events, optionally filtered by date range.
    /// </summary>
    /// <param name="query">Pagination and date filter parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paged result of events.</returns>
    public async Task<PagedResult<EventDto>> GetAllAsync(GetEventsQuery query, CancellationToken cancellationToken)
    {
        logger.LogDebug("Querying events Page={Page} PageSize={PageSize}", query.Page, query.PageSize);
        
        var from = new DateTimeOffset(
            query.From.ToDateTime(TimeOnly.MinValue),
            TimeSpan.Zero);

        var to = query.To.HasValue
            ? new DateTimeOffset(query.To.Value.ToDateTime(TimeOnly.MaxValue), TimeSpan.Zero)
            : (DateTimeOffset?)null;

        return await db.Events
            .AsNoTracking()
            .Where(e => e.EventStart >= from)
            .Where(e => to == null || e.EventStart <= to)
            .OrderBy(e => e.EventStart)
            .ToPagedResultAsync( e => new EventDto
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventStart = e.EventStart,
                EventEnd = e.EventEnd,
            }, query.Page, query.PageSize, cancellationToken);
    }
    
    /// <summary>
    /// Returns a single event by its unique identifier.
    /// </summary>
    /// <param name="eventId">The event's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The event, or null if not found.</returns>
    public async Task<EventDto?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken)
    {
        logger.LogDebug("Querying event with EventId={EventId}", eventId);

        return await db.Events
            .AsNoTracking()
            .Where(e => e.EventId == eventId)
            .Select(e => new EventDto
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventStart = e.EventStart,
                EventEnd = e.EventEnd,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="request">The event details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created event.</returns>
    public async Task<EventDto> CreateAsync(CreateEventRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Creating event with Title={Title}", request.Title);

        var newEvent = new Event
        {
            Title = request.Title,
            Description = request.Description,
            EventStart = request.EventStart,
            EventEnd = request.EventEnd,
        };

        db.Events.Add(newEvent);
        await db.SaveChangesAsync(cancellationToken);

        return new EventDto
        {
            Id = newEvent.EventId,
            Title = newEvent.Title,
            Description = newEvent.Description,
            EventStart = newEvent.EventStart,
            EventEnd = newEvent.EventEnd,
        };
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="eventId">The event's unique identifier.</param>
    /// <param name="request">The updated event details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated event.</returns>
    public async Task<EventDto?> UpdateAsync(Guid eventId, UpdateEventRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Updating event with EventId={EventId}", eventId);

        var eventToUpdate = await db.Events.FindAsync([eventId], cancellationToken);

        if (eventToUpdate is null)
        {
            logger.LogWarning("Update failed — event not found EventId={EventId}", eventId);
            return null;
        }

        eventToUpdate.Title = request.Title;
        eventToUpdate.Description = request.Description;
        eventToUpdate.EventStart = request.EventStart;
        eventToUpdate.EventEnd = request.EventEnd;

        await db.SaveChangesAsync(cancellationToken);

        return new EventDto
        {
            Id = eventToUpdate.EventId,
            Title = eventToUpdate.Title,
            Description = eventToUpdate.Description,
            EventStart = eventToUpdate.EventStart,
            EventEnd = eventToUpdate.EventEnd,
        };
    }

    /// <summary>
    /// Deletes an event by id.
    /// </summary>
    /// <param name="eventId">The event's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the event was deleted, false if not found.</returns>
    public async Task<bool> DeleteAsync(Guid eventId, CancellationToken cancellationToken)
    {
        var deleted = await db.Events
            .Where(e => e.EventId == eventId)
            .ExecuteDeleteAsync(cancellationToken);

        return deleted > 0;
    }
}
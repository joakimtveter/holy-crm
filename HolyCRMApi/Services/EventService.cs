using HolyCRMApi.Data;
using HolyCRMApi.Dtos.Events;
using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Services;

/// <summary>
/// 
/// </summary>
/// <param name="db"></param>
/// <param name="logger"></param>
public class EventService(AppDbContext db, ILogger<EventService> logger) : IEventService
{
    public async Task<List<EventDto>> GetAllAsync(int page, int pageSize)
    {
        logger.LogDebug("Querying events Page={Page} PageSize={PageSize}", page, pageSize);

        return await db.Events
            .AsNoTracking()
            .OrderBy(e => e.EventStart)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EventDto
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                EventStart = e.EventStart,
                EventEnd = e.EventEnd,
            })
            .ToListAsync();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventId">T</param>
    /// <returns>The event, or null.</returns>
    public async Task<EventDto?> GetByIdAsync(Guid eventId)
    {
        logger.LogDebug("Querying event with EventId={eventId}", eventId);

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
            .FirstOrDefaultAsync();
    }

    public async Task<EventDto> CreateAsync(CreateEventRequest request)
    {
        logger.LogDebug("Creating event with Title={Title}", request.Title);

        var newEvent = new Event
        {
            EventId = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            EventStart = request.EventStart,
            EventEnd = request.EventEnd,
        };

        db.Events.Add(newEvent);
        await db.SaveChangesAsync();

        return new EventDto
        {
            Id = newEvent.EventId,
            Title = newEvent.Title,
            Description = newEvent.Description,
            EventStart = newEvent.EventStart,
            EventEnd = newEvent.EventEnd,
        };
    }
}
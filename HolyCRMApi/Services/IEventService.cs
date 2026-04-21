using HolyCRMApi.Dtos.Events;

namespace HolyCRMApi.Services;

public interface IEventService
{
    Task<List<EventDto>> GetAllAsync(int page, int pageSize);
    Task<EventDto?> GetByIdAsync(Guid eventId);
    Task<EventDto> CreateAsync(CreateEventRequest request);
}
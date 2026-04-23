using HolyCRMApi.Dtos.Shared;
using HolyCRMApi.Dtos.Venues;

namespace HolyCRMApi.Services;

public interface IVenueService
{ 
    Task<PagedResult<VenueBriefDto>> GetAllAsync(GetVenuesQuery query, CancellationToken cancellationToken);
    
    Task<VenueDto?> GetByIdAsync(Guid venueId, CancellationToken cancellationToken, bool includeDeleted);
    
    Task<VenueDto> CreateAsync(CreateVenueRequest request, CancellationToken cancellationToken);
    
    Task<VenueDto?> UpdateAsync(Guid venueId, UpdateVenueRequest request, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(Guid venueId, CancellationToken cancellationToken);
    
    Task<bool> HardDeleteAsync(Guid venueId, CancellationToken cancellationToken);
}
using HolyCRMApi.Data;
using HolyCRMApi.Dtos.Shared;
using HolyCRMApi.Dtos.Venues;
using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Services;

/// <summary>
/// 
/// </summary>
public class VenueService(AppDbContext db, ILogger<VenueService> logger) : IVenueService
{
    public async Task<PagedResult<VenueBriefDto>> GetAllAsync(GetVenuesQuery query, CancellationToken cancellationToken)
    {
        return await db.Venues
            .AsNoTracking()
            .Where(v => v.IsDeleted == false)
            .OrderBy(v => v.Name)
            .ToPagedResultAsync( e => new VenueBriefDto
            {
                Id = e.Id,
                Name = e.Name,
                IsDeleted =  e.IsDeleted,
                UpdatedAt =  e.UpdatedAt,
                CreatedAt = e.CreatedAt,
            }, query.Page, query.PageSize, cancellationToken);
    }

    public async Task<VenueDto?> GetByIdAsync(Guid venueId, CancellationToken cancellationToken, bool includeDeleted)
    {
        return await db.Venues
            .AsNoTracking()
            .Where(e => e.Id == venueId)
            .Where(e => includeDeleted || e.IsDeleted == false)
            .Select(e => new VenueDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Address = new AddressDto
                {
                    StreetAddress = e.StreetAddress,
                    StreetAddress2 = e.StreetAddress2,
                    PostalCode = e.PostalCode,
                    City = e.City,
                    Country = e.Country
                },
                UpdatedAt = e.UpdatedAt,
                CreatedAt = e.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<VenueDto> CreateAsync(CreateVenueRequest request, CancellationToken cancellationToken)
    {
        var venue = new Venue()
        {
            Name = request.Name,
            Description = request.Description,
            StreetAddress = request.Address.StreetAddress,
            StreetAddress2 = request.Address.StreetAddress2,
            PostalCode = request.Address.PostalCode,
            City = request.Address.City,
            Country = request.Address.Country,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };
        
        db.Venues.Add(venue);
        await db.SaveChangesAsync(cancellationToken);
        return new VenueDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Description = venue.Description,
            Address = new AddressDto
            {
                StreetAddress = venue.StreetAddress,
                StreetAddress2 = venue.StreetAddress2,
                PostalCode = venue.PostalCode,
                City = venue.City,
                Country = venue.Country
            },
            CreatedAt = venue.CreatedAt,
            UpdatedAt = venue.UpdatedAt
        };
    }

    public async Task<VenueDto?> UpdateAsync(Guid venueId, UpdateVenueRequest request, CancellationToken cancellationToken)
    {
        var venue = await db.Venues.FindAsync([venueId], cancellationToken);

        if (venue is null)
        {
            logger.LogWarning("Update failed — venue with Id={VenueId} not found", venueId);
            return null;
        }

        venue.Name = request.Name;
        venue.Description = request.Description;
        venue.StreetAddress = request.Address.StreetAddress;
        venue.StreetAddress2 = request.Address.StreetAddress2;
        venue.PostalCode = request.Address.PostalCode;
        venue.City = request.Address.City;
        venue.Country = request.Address.Country;
        venue.UpdatedAt = DateTimeOffset.UtcNow;
        
        await db.SaveChangesAsync(cancellationToken);
        return new VenueDto
        {
            Id = venue.Id,
            Name = venue.Name,
            Description = venue.Description,
            Address = new AddressDto
            {
                StreetAddress = venue.StreetAddress,
                StreetAddress2 = venue.StreetAddress2,
                PostalCode = venue.PostalCode,
                City = venue.City,
                Country = venue.Country
            }, 
            UpdatedAt = venue.UpdatedAt,
            CreatedAt = venue.CreatedAt,
            IsDeleted =  venue.IsDeleted
        };
    }

    public async Task<bool> DeleteAsync(Guid venueId, CancellationToken cancellationToken)
    {
        var venueToDelete = await db.Venues.FindAsync([venueId], cancellationToken);

        if (venueToDelete is null)
        {
            logger.LogWarning("Delete failed — venue with Id={VenueId} not found", venueId);
            return false;
        }

        venueToDelete.IsDeleted = true;
        venueToDelete.UpdatedAt = DateTimeOffset.UtcNow;
        
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> HardDeleteAsync(Guid venueId, CancellationToken cancellationToken)
    {
        var deleted = await db.Venues
            .Where(v => v.Id == venueId)
            .ExecuteDeleteAsync(cancellationToken);
        
        return deleted > 0;
    }
}
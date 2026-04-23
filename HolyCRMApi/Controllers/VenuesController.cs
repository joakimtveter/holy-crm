using HolyCRMApi.Dtos.Shared;
using HolyCRMApi.Dtos.Venues;
using HolyCRMApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolyCRMApi.Controllers;

/// <summary>
///  Venues are locations where events takes place
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class VenuesController(IVenueService venueService, ILogger<VenuesController> logger) : ControllerBase
{
    [HttpGet(Name = "GetVenues")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<VenueBriefDto>>> GetVenues(
        [FromQuery] GetVenuesQuery query,  CancellationToken cancellationToken)
    {
        logger.LogDebug("GetVenues");

        var venues = await venueService.GetAllAsync(query, cancellationToken);
        
        logger.LogDebug("Found {Count} venues. Returned page {PageNumber} of {TotalPages}", venues.TotalCount, venues.CurrentPage, venues.TotalPages);

        return Ok(venues);
    }

    [HttpGet("{venueId:guid}", Name = "GetVenueById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VenueDto>> GetVenueById(
        [FromRoute] Guid venueId,
        [FromQuery] bool includeDeleted = false,
        CancellationToken cancellationToken = default
        )
    {
        logger.LogDebug("Fetching venue with Id={venueId}, include deleted is {IncludeDeleted}.", venueId,  includeDeleted);

        var venue = await venueService.GetByIdAsync(venueId, cancellationToken,  includeDeleted);
        
        if  (venue is null)
        {
            logger.LogWarning("Venue not found with VenueId={venueId}", venueId);
            return NotFound();
        }
        
        return Ok(venue);
    }

    [HttpPost(Name = "CreateVenue")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VenueDto>> CreateVenue(
        [FromBody] CreateVenueRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("Creating venue");
        var newVenue = await venueService.CreateAsync(request, cancellationToken);
        return CreatedAtRoute("GetVenueById", new { venueId = newVenue.Id }, newVenue);
    }


    [HttpPut("{venueId:guid}", Name = "UpdateVenue")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VenueDto>> UpdateVenue(
        [FromRoute] Guid venueId,
        [FromBody] UpdateVenueRequest request,
        CancellationToken cancellationToken)
    {
        var updatedVenue = await venueService.UpdateAsync(venueId, request, cancellationToken);

        if (updatedVenue is null)
        {
            logger.LogWarning("Venue not found with VenueId={venueId}", venueId);
            return NotFound();
        }
        
        return Ok(updatedVenue);
    }

    [HttpDelete("{venueId:guid}", Name = "DeleteVenue")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteVenue([FromRoute] Guid venueId, CancellationToken cancellationToken)
    {
        logger.LogDebug("Deleting venue with Id={VenueId}", venueId);
        
        var deleted = await venueService.DeleteAsync(venueId, cancellationToken);
        
        if (!deleted)
        {
            logger.LogWarning("Could not delete venue. Venue with Id={venueId} not found.", venueId);
            return NotFound();
        }
        return NoContent();
    }    
    
    [HttpDelete("{venueId:guid}/permanent", Name = "HardDeleteVenue")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> HardDeleteVenue([FromRoute] Guid venueId, CancellationToken cancellationToken)
    {
        logger.LogDebug("Hard deleting venue with Id={VenueId}", venueId);
        
        var deleted = await venueService.HardDeleteAsync(venueId, cancellationToken);
        
        if (!deleted)
        {
            logger.LogWarning("Could not delete venue. Venue with Id={VenueId} not found.", venueId);
            return NotFound();
        }
        return NoContent();
    }

}
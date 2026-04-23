using HolyCRMApi.Dtos.Shared;

namespace HolyCRMApi.Dtos.Venues;

/// <summary>
/// 
/// </summary>
public class VenueBriefDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Name {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsDeleted {get; set;} = false;
    
    /// <summary>
    /// UTC timestamp when the venue was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// UTC timestamp when the venue was last updated.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}

/// <summary>
/// 
/// </summary>
public class VenueDto : VenueBriefDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Description {get; set;} = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public AddressDto Address { get; init; }
    
    
}
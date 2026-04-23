using System.ComponentModel.DataAnnotations;
using HolyCRMApi.Dtos.Shared;

namespace HolyCRMApi.Dtos.Venues;

/// <summary>
/// 
/// </summary>
public class CreateVenueRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(250)]
    public string Description {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public AddressDto Address { get; set; } = new AddressDto();
}
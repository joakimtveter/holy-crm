using System.ComponentModel.DataAnnotations;

namespace HolyCRMApi.Models;

/// <summary>
/// 
/// </summary>
public class Event
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Title {get; set;} = string.Empty;
    
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
    public DateTimeOffset StartsAt {get; set;}

    /// <summary>
    ///
    /// </summary>
    [Required]
    public DateTimeOffset EndsAt {get; set;}

    /// <summary>
    ///
    /// </summary>
    [Required]
    public Guid VenueId {get; set;}
    
    /// <summary>
    ///
    /// </summary>
    [Required]
    public Venue Venue {get; set;}
    
    [Required]
    public bool IsDeleted {get; set;} = false;
}
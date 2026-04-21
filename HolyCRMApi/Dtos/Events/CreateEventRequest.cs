using System.ComponentModel.DataAnnotations;

namespace HolyCRMApi.Dtos.Events;

/// <summary>
/// 
/// </summary>
public class CreateEventRequest
{
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
    public DateTimeOffset EventStart {get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public DateTimeOffset EventEnd {get; set;}
    
}
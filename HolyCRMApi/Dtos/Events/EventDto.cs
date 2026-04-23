using HolyCRMApi.Dtos.Shared;

namespace HolyCRMApi.Dtos.Events;

/// <summary>
/// 
/// </summary>
public class EventBriefDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Title {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public string Description {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset StartsAt {get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset EndsAt {get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    public string Venue {get; set;} = string.Empty;
}

/// <summary>
/// 
/// </summary>
public class EventDto : EventBriefDto
{
 /// <summary>
 /// 
 /// </summary>
 public AddressDto Address {get; set;}
}
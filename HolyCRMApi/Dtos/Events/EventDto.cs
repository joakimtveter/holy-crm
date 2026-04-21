namespace HolyCRMApi.Dtos.Events;

/// <summary>
/// 
/// </summary>
public class EventDto
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
    public DateTimeOffset EventStart {get; set;}
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset EventEnd {get; set;}
}
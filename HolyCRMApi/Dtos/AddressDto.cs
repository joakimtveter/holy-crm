namespace HolyCRMApi.Dtos;

/// <summary>
/// 
/// </summary>
public class AddressDto
{
    /// <summary>
    /// 
    /// </summary>
    public string Address1 { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public string? Address2 { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string PostalCode { get; set; } = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public string City { get; set; }  = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    public string? Country { get; set; }
}
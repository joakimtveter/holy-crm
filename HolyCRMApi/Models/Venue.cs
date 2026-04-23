using System.ComponentModel.DataAnnotations;

namespace HolyCRMApi.Models;

/// <summary>
/// 
/// </summary>
public class Venue
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
    public string Name {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(250)]
    public string Description {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string StreetAddress {get; set;} = string.Empty;
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(100)]
    public string? StreetAddress2 {get; set;}

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string PostalCode {get; set;} = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string City {get; set;} = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(60)]
    public string? Country {get; set;}
    
    /// <summary>
    /// UTC timestamp when the venue was created.
    /// </summary>
    [Required]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// UTC timestamp when the venue was last updated.
    /// </summary>
    [Required]
    public DateTimeOffset UpdatedAt { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public bool IsDeleted {get; set;} = false;
}
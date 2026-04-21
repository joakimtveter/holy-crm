using System.ComponentModel.DataAnnotations;

namespace HolyCRMApi.Dtos;

/// <summary>
/// 
/// </summary>
public class GetEventsQuery
{
    /// <summary>
    /// Page number to retrieve (1-based). Defaults to 1.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of members per page (1–100). Defaults to 15.
    /// </summary>
    [Range(1, 100)]
    public int PageSize { get; set; } = 15;
}
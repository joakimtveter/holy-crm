using System.ComponentModel.DataAnnotations;

namespace HolyCRMApi.Dtos.Venues;

public class GetVenuesQuery
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    
    [Range(1, 200)]
    public int PageSize { get; set; } = 100;

}
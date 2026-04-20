namespace HolyCRMApi.Dtos;

public class UpdateMemberRequest
{
    public string FirstName { get; set; } = string.Empty;
    
    public string? MiddleNames { get; set; }
    
    public string LastName { get; set; } = string.Empty;
    
    public DateOnly? DateOfBirth { get; set; }
}
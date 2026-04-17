namespace HolyCRMApi.Models;

public class Member
{
    public Guid MemberId { get; set; }
    
    public string FirstName { get; set; } =  string.Empty;
    
    public string? MiddleNames {get; set;}
    
    public string LastName { get; set; } =  string.Empty;
    
    public DateOnly? DateOfBirth { get; set; }
    
}
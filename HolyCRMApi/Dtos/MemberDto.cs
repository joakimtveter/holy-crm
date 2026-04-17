namespace HolyCRMApi.Models;

public class MemberDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string? MiddleName { get; set; }
    
    public string LastName { get; set; } = string.Empty;
    
    
}
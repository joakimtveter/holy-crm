namespace HolyCRMApi.Models;

public class CreateMemberDto
{
    public string FirstName { get; set; } = string.Empty;
    
    public string? MiddleName { get; set; }
    
    public string LastName { get; set; } = string.Empty;
    
    public string? Email { get; set; } = string.Empty;
    
    
}
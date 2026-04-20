namespace HolyCRMApi.Dtos;

/// <summary>
/// Represents a member returned from the API.
/// </summary>
public class MemberDto
{
    /// <summary>
    /// Unique identifier for the member.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// First name of the member.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Middle name(s) of the member, if any.
    /// </summary>
    public string? MiddleNames { get; set; }

    /// <summary>
    /// Last name of the member.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Date of birth of the member, if known.
    /// </summary>
    public string? DateOfBirth { get; set; }
}
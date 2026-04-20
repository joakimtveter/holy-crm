namespace HolyCRMApi.Dtos;

/// <summary>
/// Request body for updating an existing member.
/// </summary>
public class UpdateMemberRequest
{
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
    public DateOnly? DateOfBirth { get; set; }
}
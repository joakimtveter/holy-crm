using System.ComponentModel.DataAnnotations;
using HolyCRMApi.Enums;

namespace HolyCRMApi.Dtos;

/// <summary>
/// Request body for updating an existing member.
/// </summary>
public class UpdateMemberRequest
{
    /// <summary>
    /// First name of the member.
    /// </summary>
    [Required]
    [MaxLength(250)]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Middle name(s) of the member, if any.
    /// </summary>
    [MaxLength(250)]
    public string? MiddleNames { get; init; }

    /// <summary>
    /// Last name of the member.
    /// </summary>
    [Required]
    [MaxLength(250)]
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Date of birth of the member, if known.
    /// </summary>
    public DateOnly? DateOfBirth { get; init; }

    /// <summary>
    /// Gender of the member.
    /// </summary>
    public Gender Gender { get; init; }
}
using HolyCRMApi.Dtos;
using HolyCRMApi.Dtos.Members;
using HolyCRMApi.Dtos.Shared;

namespace HolyCRMApi.Services;

/// <summary>
/// Defines the contract for member management operations.
/// </summary>
public interface IMemberService
{
    /// <summary>
    /// Returns a paginated list of members ordered by last name then first name by default.
    /// </summary>
    /// <param name="query">Pagination and filter parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paged result of members.</returns>
    Task<PagedResult<MemberBriefDto>> GetAllAsync(GetMembersQuery query, CancellationToken cancellationToken);

    /// <summary>
    /// Returns a single member by their unique identifier.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The member, or null if not found.</returns>
    Task<MemberDto?> GetByIdAsync(Guid memberId, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new member.
    /// </summary>
    /// <param name="request">The member details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created member.</returns>
    Task<MemberDto> CreateAsync(CreateMemberRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing member.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <param name="request">The updated member details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated member, or null if not found.</returns>
    Task<MemberDto?> UpdateAsync(Guid memberId, UpdateMemberRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a member by their unique identifier.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the member was deleted, false if not found.</returns>
    Task<bool> DeleteAsync(Guid memberId, CancellationToken cancellationToken);
}
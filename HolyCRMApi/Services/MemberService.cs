using HolyCRMApi.Data;
using HolyCRMApi.Dtos;
using HolyCRMApi.Dtos.Members;
using HolyCRMApi.Dtos.Shared;
using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Services;

/// <summary>
/// Handles data access and business logic for members.
/// </summary>
public class MemberService(AppDbContext db, ILogger<MemberService> logger) : IMemberService
{
    /// <summary>
    /// Returns a paginated list of all members ordered by last name then first name.
    /// </summary>
    /// <param name="query">Filtering and pagination options.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>Paged list of members.</returns>
    public async Task<PagedResult<MemberBriefDto>> GetAllAsync(GetMembersQuery query,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("Querying members Page={Page} PageSize={PageSize}", query.Page, query.PageSize);

        return await db.Members
            .AsNoTracking()
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .ToPagedResultAsync(m => new MemberBriefDto
            {
                Id = m.Id,
                FirstName = m.FirstName,
                MiddleNames = m.MiddleNames,
                LastName = m.LastName,
                DateOfBirth = m.DateOfBirth,
                Gender = m.Gender,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
            }, query.Page, query.PageSize, cancellationToken);
    }

    /// <summary>
    /// Returns a single member by their ID, or null if not found.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The member, or null.</returns>
    public async Task<MemberDto?> GetByIdAsync(Guid memberId, CancellationToken cancellationToken)
    {
        logger.LogDebug("Querying member MemberId={MemberId}", memberId);

        var member = await db.Members
            .AsNoTracking()
            .Where(m => m.Id == memberId)
            .FirstOrDefaultAsync(cancellationToken);

        return member is null ? null : ToDto(member);
    }

    /// <summary>
    /// Updates an existing member. Returns the updated member, or null if not found.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <param name="request">The updated member data.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The updated member, or null.</returns>
    public async Task<MemberDto?> UpdateAsync(Guid memberId, UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        logger.LogDebug("Updating member MemberId={MemberId}", memberId);

        var member = await db.Members.FindAsync([memberId], cancellationToken);

        if (member is null)
        {
            logger.LogWarning("Update failed — member not found MemberId={MemberId}", memberId);
            return null;
        }

        member.FirstName = request.FirstName.Trim();
        member.MiddleNames = request.MiddleNames?.Trim();
        member.LastName = request.LastName.Trim();
        member.DateOfBirth = request.DateOfBirth;
        member.Gender = request.Gender;
        member.UpdatedAt = DateTimeOffset.UtcNow;

        await db.SaveChangesAsync(cancellationToken);

        return ToDto(member);
    }

    /// <summary>
    /// Creates a new member and returns the created member.
    /// </summary>
    /// <param name="request">The new member data.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>The newly created member.</returns>
    public async Task<MemberDto> CreateAsync(CreateMemberRequest request, CancellationToken cancellationToken)
    {
        logger.LogDebug("Creating member");

        var now = DateTimeOffset.UtcNow;
        var member = new Member
        {
            FirstName = request.FirstName.Trim(),
            MiddleNames = request.MiddleNames?.Trim(),
            LastName = request.LastName.Trim(),
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            CreatedAt = now,
            UpdatedAt = now,
        };

        db.Members.Add(member);
        await db.SaveChangesAsync(cancellationToken);

        return ToDto(member);
    }

    /// <summary>
    /// Deletes a member by ID. Returns true if deleted, false if not found.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if the member was deleted, false if not found.</returns>
    public async Task<bool> DeleteAsync(Guid memberId, CancellationToken cancellationToken)
    {
        var deleted = await db.Members
            .Where(m => m.Id == memberId)
            .ExecuteDeleteAsync(cancellationToken);

        return deleted > 0;
    }

    private static MemberDto ToDto(Member m) => new()
    {
        Id = m.Id,
        FirstName = m.FirstName,
        MiddleNames = m.MiddleNames,
        LastName = m.LastName,
        DateOfBirth = m.DateOfBirth,
        Gender = m.Gender,
        CreatedAt = m.CreatedAt,
        UpdatedAt = m.UpdatedAt,
    };
}

using HolyCRMApi.Data;
using HolyCRMApi.Dtos;
using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Services;

/// <summary>
/// Handles data access and business logic for members.
/// </summary>
/// <param name="db"></param>
public class MemberService(AppDbContext db, ILogger<MemberService> logger) : IMemberService
{
    /// <summary>
    /// Returns a paginated list of all members ordered by last name then first name.
    /// </summary>
    /// <param name="page">1-based page number.</param>
    /// <param name="pageSize">Number of members per page.</param>
    /// <returns>List of members for the requested page.</returns>
    public async Task<List<MemberDto>> GetAllAsync(int page, int pageSize)
    {
        logger.LogDebug("Querying members Page={Page} PageSize={PageSize}", page, pageSize);

        return await db.Members
            .AsNoTracking()
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MemberDto
            {
                Id = m.MemberId,
                FirstName = m.FirstName,
                MiddleNames = m.MiddleNames,
                LastName  = m.LastName,
                DateOfBirth = m.DateOfBirth,
                Gender = m.Gender,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
            })
            .ToListAsync();
    }  
    
    /// <summary>
    /// Returns a single member by their ID, or null if not found.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <returns>The member, or null.</returns>
    public async Task<MemberDto?> GetByIdAsync(Guid memberId)
    {
        logger.LogDebug("Querying member MemberId={MemberId}", memberId);

        return await db.Members
            .AsNoTracking()
            .Where(m => m.MemberId == memberId)
            .Select(m => new MemberDto
            {
                Id = m.MemberId,
                FirstName = m.FirstName,
                MiddleNames = m.MiddleNames,
                LastName  = m.LastName,
                DateOfBirth = m.DateOfBirth,
                Gender = m.Gender,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
            })
            .FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// Updates an existing member. Returns the updated member, or null if not found.
    /// </summary>
    /// <param name="id">The member's unique identifier.</param>
    /// <param name="request">The updated member data.</param>
    /// <returns>The updated member, or null.</returns>
    public async Task<MemberDto?> UpdateAsync(Guid id, UpdateMemberRequest request)
    {
        logger.LogDebug("Updating member MemberId={MemberId}", id);

        var member = await db.Members.FindAsync(id);

        if (member is null)
        {
            logger.LogWarning("Update failed — member not found MemberId={MemberId}", id);
            return null;
        }

        member.FirstName = request.FirstName.Trim();
        member.MiddleNames = request.MiddleNames?.Trim();
        member.LastName = request.LastName.Trim();
        member.DateOfBirth = request.DateOfBirth;
        member.Gender = request.Gender;
        member.UpdatedAt = DateTimeOffset.UtcNow;

        await db.SaveChangesAsync();

        return new MemberDto
        {
            Id = member.MemberId,
            FirstName = member.FirstName,
            MiddleNames = member.MiddleNames,
            LastName = member.LastName,
            DateOfBirth = member.DateOfBirth,
            Gender = member.Gender,
            CreatedAt = member.CreatedAt,
            UpdatedAt = member.UpdatedAt,
        };
    }
    
    /// <summary>
    /// Creates a new member and returns the created member.
    /// </summary>
    /// <param name="request">The new member data.</param>
    /// <returns>The newly created member.</returns>
    public async Task<MemberDto> CreateAsync(CreateMemberRequest request)
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
        await db.SaveChangesAsync();

        return new MemberDto
        {
            Id = member.MemberId,
            FirstName = member.FirstName,
            MiddleNames = member.MiddleNames,
            LastName = member.LastName,
            DateOfBirth = member.DateOfBirth,
            Gender = member.Gender,
            CreatedAt = member.CreatedAt,
            UpdatedAt = member.UpdatedAt,
        };
    }
    
    /// <summary>
    /// Deletes a member by ID. Returns true if deleted, false if not found.
    /// </summary>
    /// <param name="memberId">The member's unique identifier.</param>
    /// <returns>True if the member was deleted, false if not found.</returns>
    public async Task<bool> DeleteAsync(Guid memberId)
    {
        logger.LogDebug("Deleting member MemberId={MemberId}", memberId);

        var member = await db.Members.FindAsync(memberId);

        if (member == null)
        {
            logger.LogWarning("Delete failed — member not found MemberId={MemberId}", memberId);
            return false;
        }
        
        db.Members.Remove(member);
        await db.SaveChangesAsync();

        return true;
    }
}

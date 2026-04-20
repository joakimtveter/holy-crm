using HolyCRMApi.Data;
using HolyCRMApi.Dtos;
using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Services;

/// <summary>
/// 
/// </summary>
/// <param name="db"></param>
public class MemberService(AppDbContext db)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<MemberDto>> GetAllAsync(int page, int pageSize)
    {
        return await db.Members
            .AsNoTracking()
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .Take(pageSize)
            .Skip((page - 1) * pageSize)
            .Select(m => new MemberDto
            {
                Id = m.MemberId,
                FirstName = m.FirstName,
                MiddleNames = m.MiddleNames,
                LastName  = m.LastName,
                DateOfBirth = m.DateOfBirth.ToString(),
            })
            .ToListAsync();
    }  
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<MemberDto?> GetByIdAsync(Guid memberId)
    {
        return await db.Members
            .AsNoTracking()
            .Where(m => m.MemberId == memberId)
            .Select(m => new MemberDto
            {
                Id = m.MemberId,
                FirstName = m.FirstName,
                MiddleNames = m.MiddleNames,
                LastName  = m.LastName,
                DateOfBirth = m.DateOfBirth.ToString(),
            })
            .FirstOrDefaultAsync();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<MemberDto?> UpdateAsync(Guid id, UpdateMemberRequest request)
    {
        var member = await db.Members.FindAsync(id);

        if (member is null) return null;

        member.FirstName = request.FirstName.Trim();
        request.MiddleNames = request.MiddleNames?.Trim();
        member.LastName = request.LastName.Trim();
        member.DateOfBirth = request.DateOfBirth;

        await db.SaveChangesAsync();

        return new MemberDto
        {
            Id = member.MemberId,
            FirstName = member.FirstName,
            MiddleNames = member.MiddleNames,
            LastName = member.LastName,
            DateOfBirth = member.DateOfBirth.ToString(),
        };
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<MemberDto> CreateAsync(CreateMemberRequest request)
    {
        var member = new Member
        {
            FirstName = request.FirstName.Trim(),
            MiddleNames = request.MiddleNames?.Trim(),
            LastName = request.LastName.Trim(),
            DateOfBirth = request.DateOfBirth
        };

        db.Members.Add(member);
        await db.SaveChangesAsync();

        return new MemberDto
        {
            Id = member.MemberId,
            FirstName = member.FirstName,
            MiddleNames = member.MiddleNames,
            LastName = member.LastName,
            DateOfBirth = member.DateOfBirth.ToString(),
        };
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(Guid memberId)
    {
        var member = await db.Members.FindAsync(memberId);

        if (member == null) return false;
        
        db.Members.Remove(member);
        await db.SaveChangesAsync();

        return true;
    }
}

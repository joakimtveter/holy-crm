using HolyCRMApi.Dtos;

namespace HolyCRMApi.Services;

public interface IMemberService
{
    public Task<List<MemberDto>> GetAllAsync(int page, int pageSize);

    public Task<MemberDto?> GetByIdAsync(Guid memberId);

    public Task<MemberDto?> UpdateAsync(Guid id, UpdateMemberRequest request);

    public Task<MemberDto> CreateAsync(CreateMemberRequest request);

    public Task<bool> DeleteAsync(Guid memberId);

}
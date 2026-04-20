using HolyCRMApi.Dtos;
using HolyCRMApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolyCRMApi.Controllers;

/// <summary>
/// Members are people connected to your church
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MembersController(
    MemberService memberService,
    ILogger<MembersController> logger
    ) : ControllerBase
{

    /// <summary>
    /// Get all members
    /// </summary>
    /// <returns>List of Members</returns>
    [HttpGet(Name = "GetMembers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<MemberDto>>> GetMembers([FromQuery] GetMembersQuery query)
    {
        var members = await memberService.GetAllAsync(query.Page, query.PageSize);

        return Ok(members);
    }

    /// <summary>
    /// Get member by memberId
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    [HttpGet("{memberId:guid}", Name = "GetMemberById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> GetMemberById([FromRoute] Guid memberId)
    {
        var member = await memberService.GetByIdAsync(memberId);
        
        if (member is null) return  NotFound();
        
        return Ok(member);
    }
    
    /// <summary>
    /// Create a new member
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateMember")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemberDto>> CreateMember([FromBody]CreateMemberRequest request)
    {
        var createdMember = await memberService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetMemberById),
            new { memberId = createdMember.Id },
            createdMember
        );
    }

    /// <summary>
    /// Update member
    /// </summary>
    /// <param name="memberId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{memberId:guid}", Name = "UpdateMember")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> UpdateMember(
        [FromRoute] Guid memberId,
        [FromBody] UpdateMemberRequest request
        )
    {
        var updatedMember = await memberService.UpdateAsync(memberId, request);
        if (updatedMember == null) return NotFound();
        return Ok(updatedMember);
    }

    /// <summary>
    /// Delete member by memberId
    /// </summary>
    /// <param name="memberId"></param>
    /// <returns></returns>
    [HttpDelete("{memberId:guid}", Name = "DeleteMember")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteMember([FromRoute] Guid memberId)
    {
        var member = await memberService.DeleteAsync(memberId);
        if  (!member) return NotFound();
        return NoContent();
    }
}
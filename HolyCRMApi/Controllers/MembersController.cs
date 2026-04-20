using HolyCRMApi.Dtos;
using HolyCRMApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HolyCRMApi.Controllers;

/// <summary>
/// Members are people connected to your church
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
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
        logger.LogDebug("Fetching members on Page={Page} with PageSize={PageSize}", query.Page, query.PageSize);

        var members = await memberService.GetAllAsync(query.Page, query.PageSize);

        logger.LogDebug("Returned {Count} members", members.Count);

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
        logger.LogDebug("Fetching member MemberId={MemberId}", memberId);

        var member = await memberService.GetByIdAsync(memberId);

        if (member is null)
        {
            logger.LogWarning("Member not found MemberId={MemberId}", memberId);
            return NotFound();
        }

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
        logger.LogDebug("Creating member");

        var createdMember = await memberService.CreateAsync(request);

        logger.LogInformation("Member created MemberId={MemberId}", createdMember.Id);

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
        logger.LogDebug("Updating member MemberId={MemberId}", memberId);

        var updatedMember = await memberService.UpdateAsync(memberId, request);

        if (updatedMember == null)
        {
            logger.LogWarning("Member not found MemberId={MemberId}", memberId);
            return NotFound();
        }

        logger.LogInformation("Member updated MemberId={MemberId}", memberId);

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
        logger.LogDebug("Deleting member MemberId={MemberId}", memberId);

        var deleted = await memberService.DeleteAsync(memberId);

        if (!deleted)
        {
            logger.LogWarning("Member not found MemberId={MemberId}", memberId);
            return NotFound();
        }

        logger.LogInformation("Member deleted MemberId={MemberId}", memberId);

        return NoContent();
    }
}

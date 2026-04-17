using HolyCRMApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyCRMApi.Controllers;

/// <summary>
/// Members are people connected to your church
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MemberController(ILogger<MemberController> logger) : ControllerBase
{

    /// <summary>
    /// Get all members
    /// </summary>
    /// <returns>List of Members</returns>
    [HttpGet(Name = "GetMembers")]
    public ActionResult<IEnumerable<MemberDto>> GetMembers( int page = 1, int pageSize = 15)
    {
        logger.LogInformation("GetMembers called");
        var membersToReturn = new List<MemberDto>();

        return Ok(membersToReturn);
    }
}
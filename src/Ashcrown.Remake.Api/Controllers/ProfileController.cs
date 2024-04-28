using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProfileController(IPlayerSessionService playerSessionService) : ControllerBase
{
    [HttpPut("update/icon", Name = nameof(UpdateIcon))]
    [ProducesResponseType(typeof(ActionResult<PlayerSession>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlayerSession>> UpdateIcon([FromBody] PlayerRequestSelectProfileIcon playerRequest)
    {
        await playerSessionService.UpdateSession(playerRequest.Name, playerRequest.Secret, session => session.IconName = playerRequest.IconName);
        var playerSession = await playerSessionService.GetSession(playerRequest.Name);
        return Ok(playerSession);
    }
}
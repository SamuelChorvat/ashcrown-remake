using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class LoginController(IPlayerSessionService playerSessionService) : ControllerBase
{
    [HttpGet("version", Name = nameof(GetVersionInfo))]
    [ProducesResponseType(typeof(VersionInfo), StatusCodes.Status200OK)]
    public Task<VersionInfo> GetVersionInfo()
    {
        return Task.FromResult(new VersionInfo());
    }

    [HttpPost("session/create")]
    [ProducesResponseType(typeof(PlayerSession), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlayerSession>> CreatePlayerSession([FromBody] string playerName)
    {
        if (!await playerSessionService.CreateSession(playerName))
            return Conflict($"The playerName '{playerName}' is currently taken.");
        
        var playerSession = await playerSessionService.GetSession(playerName);
        if (playerSession != null)
        {
            return playerSession;
        }

        return StatusCode(500, "Something went wrong!");
    }
    
    [HttpGet("session/list", Name = nameof(ListCurrentSessionNames))]
    [ProducesResponseType(typeof(IList<string>), StatusCodes.Status200OK)]
    public Task<IList<string>> ListCurrentSessionNames()
    {
        return playerSessionService.GetCurrentInUsePlayerNames();
    }
    
    [HttpGet("session/refresh/{playerName}", Name = nameof(RefreshSession))]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlayerSession>> RefreshSession(string playerName)
    {
        await playerSessionService.UpdateSession(playerName, session => session.LastRequestDateTime = DateTime.UtcNow);
        var playerSession = await playerSessionService.GetSession(playerName);
        return Ok(playerSession);
    }
}

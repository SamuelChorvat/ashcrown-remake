using Ashcrown.Remake.Api.Dtos.Outbound;
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> CreatePlayerSession([FromBody] string playerName)
    {
        if (await playerSessionService.CreateSession(playerName))
        {
            return Created();
        }

        return Conflict($"The playerName '{playerName}' is currently taken.");
    }
    
    [HttpGet("session/list", Name = nameof(ListCurrentSessionNames))]
    [ProducesResponseType(typeof(IList<string>), StatusCodes.Status200OK)]
    public Task<IList<string>> ListCurrentSessionNames()
    {
        return playerSessionService.GetCurrentInUsePlayerNames();
    }
}

using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class SessionController(IPlayerSessionService playerSessionService) : ControllerBase
{
    [HttpPost("create", Name = nameof(CreatePlayerSession))]
    [ProducesResponseType(typeof(PlayerSession), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlayerSession>> CreatePlayerSession([FromBody] PlayerRequest playerRequest)
    {
        var session = await playerSessionService.CreateSession(playerRequest.Name);

        if (session == null)
        {
            return Conflict($"The playerName '{playerRequest.Name}' is currently taken");
        }

        return Ok(session);
    }
    
    [HttpGet("list/players", Name = nameof(ListCurrentSessionNames))]
    [ProducesResponseType(typeof(IList<string>), StatusCodes.Status200OK)]
    public Task<IList<string>> ListCurrentSessionNames()
    {
        return playerSessionService.GetCurrentInUsePlayerNames();
    }
    
    [HttpPut("refresh", Name = nameof(RefreshSession))]
    [ProducesResponseType(typeof(ActionResult<PlayerSession>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PlayerSession>> RefreshSession([FromBody] PlayerRequest playerRequest)
    {
        await playerSessionService.UpdateSession(playerRequest.Name, playerRequest.Secret, session => session.LastRequestDateTime = DateTime.UtcNow);
        var playerSession = await playerSessionService.GetSessionAsync(playerRequest.Name);
        return Ok(playerSession);
    }
    
    [HttpDelete("delete", Name = nameof(DeletePlayerSession))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePlayerSession([FromBody] PlayerRequest playerRequest)
    {
        if (!await playerSessionService.RemoveSession(playerRequest.Name, playerRequest.Secret))
        {
            return NotFound($"No session for '{playerRequest.Name}' exists");
        }

        return Ok($"Deleted session for '{playerRequest.Name}'");
    }
    
    [HttpGet("{playerName}", Name = nameof(GetPlayerSession))]
    [ProducesResponseType(typeof(PlayerSession), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlayerSession>> GetPlayerSession(string playerName)
    {
        var session = await playerSessionService.GetSessionAsync(playerName);
        if (session != null)
        {
            return Ok(session);
        } 
        return NotFound($"No session for '{playerName}' exists");;
    }
}
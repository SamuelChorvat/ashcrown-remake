using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class MatchController(IPlayerSessionService playerSessionService, 
    IMatchmakerService matchmakerService) : ControllerBase
{
    [HttpPost("find", Name = nameof(FindMatch))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> FindMatch([FromBody] PlayerRequestFindMatch playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        if (await matchmakerService.AddToMatchmaking(playerRequest.Name, playerRequest.MatchType, playerRequest.PrivateOpponentName))
        {
            return Ok();
        }

        return BadRequest();
    }
    
    [HttpDelete("cancel", Name = nameof(CancelFindMatch))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelFindMatch([FromBody] PlayerRequest playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);

        if (await matchmakerService.RemoveFromMatchMaking(playerRequest.Name))
        {
            return Ok();
        }
        
        return NotFound();
    }
}

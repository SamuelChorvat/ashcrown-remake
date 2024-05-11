using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
    
    [HttpPost("getMatched", Name = nameof(GetMatched))]
    [ProducesResponseType(typeof(FoundMatchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FoundMatchResponse>> GetMatched([FromBody] PlayerRequest playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        var foundMatchResponse = await matchmakerService.TryToMatchPlayer(playerRequest.Name);
        if (foundMatchResponse != null)
        {
            return Ok(foundMatchResponse);
        }

        return NotFound();
    }
    
    [HttpPut("accept", Name = nameof(AcceptMatch))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> AcceptMatch([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        await matchmakerService.AcceptMatch(playerRequest.Name, playerRequest.MatchId);
        return Ok();
    }
    
    [HttpDelete("decline", Name = nameof(DeclineMatch))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeclineMatch([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        await matchmakerService.DeclineMatch(playerRequest.MatchId);
        return Ok();
    }
    
    [HttpPut("found", Name = nameof(GetFoundStatus))]
    [ProducesResponseType(typeof(FoundMatchResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<FoundMatchStatusUpdate>> GetFoundStatus([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        return new FoundMatchStatusUpdate
        {
            FoundMatchStatus = await matchmakerService.GetFoundMatchStatus(playerRequest.MatchId)
        };
    }
}

using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class BattleController(IPlayerSessionService playerSessionService, 
    IBattleService battleService) : ControllerBase
{
    [HttpPut("ready", Name = nameof(ReadyMatch))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> ReadyMatch([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        await battleService.StartAcceptedMatchBattle(playerRequest.MatchId, playerRequest.Name);
        return Ok();
    }
    
    [HttpPut("ready/status", Name = nameof(ReadyStatus))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<AcceptedMatchStatusUpdate> ReadyStatus([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);

        return new AcceptedMatchStatusUpdate
        {
            AcceptedMatchStatus = await battleService.GetAcceptedMatchBattleStatus(playerRequest.MatchId)
        };
    }
}
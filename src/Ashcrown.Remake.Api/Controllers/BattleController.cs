using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Models.Enums;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Battle;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;
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
    [ProducesResponseType(typeof(AcceptedMatchStatusUpdate), StatusCodes.Status200OK)]
    public async Task<AcceptedMatchStatusUpdate> ReadyStatus([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);

        return new AcceptedMatchStatusUpdate
        {
            AcceptedMatchStatus = await battleService.GetAcceptedMatchBattleStatus(playerRequest.MatchId)
        };
    }
    
    [HttpGet("update/{matchId}/{playerName}", Name = nameof(BattleUpdate))]
    [ProducesResponseType(typeof(BattleStatusUpdate), StatusCodes.Status200OK)]
    public async Task<BattleStatusUpdate> BattleUpdate(string matchId, string playerName)
    {
        var startedMatch = await battleService.GetStartedMatch(Guid.Parse(matchId));
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        var playerNo = battleLogic.GetBattlePlayerNo(playerName);
        
        if (battleLogic.BattleEndedUpdates is not null)
        {
            return new BattleStatusUpdate
            {
                BattleStatus = battleLogic.BattleEndedUpdates[playerNo - 1]
            };
        }

        if (battleLogic.TurnStartTime.AddSeconds(BattleConstants.TurnTimeInSeconds + 3) < DateTime.UtcNow)
        {
            battleLogic.EndTurnProcesses(battleLogic.WhoseTurn.PlayerNo);
        }

        return new BattleStatusUpdate
        {
            BattleStatus = battleLogic.WhoseTurn.PlayerNo == playerNo
                ? BattleStatus.YourTurn
                : BattleStatus.OpponentsTurn,
            PlayerUpdate = battleLogic.LatestPlayerUpdates[playerNo - 1]
        };
    }
}
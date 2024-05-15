using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Battle;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class BattleController(IPlayerSessionService playerSessionService, 
    IBattleService battleService, IMatchHistoryService matchHistoryService,
    ILogger<BattleController> logger) : ControllerBase
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
        
        if (!battleLogic.AiBattle
            && await playerSessionService.GetSessionAsync(battleLogic.GetOppositePlayer(playerNo).PlayerName) == null)
        {
            battleLogic.Surrender(battleLogic.GetOppositePlayer(playerNo).PlayerNo);
            await matchHistoryService.AddBattleToMatchHistory(startedMatch);
        }
        
        if (battleLogic.BattleEndedUpdates is not null)
        {
            await matchHistoryService.AddBattleToMatchHistory(startedMatch);
            return new BattleStatusUpdate
            {
                BattleStatus = battleLogic.BattleEndedUpdates[playerNo - 1]
            };
        }

        if (battleLogic.TurnStartTime.AddSeconds(BattleConstants.TurnTimeInSeconds + 3) < DateTime.UtcNow)
        {
            try
            {
                battleLogic.EndTurnProcesses(battleLogic.WhoseTurn.PlayerNo);
            }
            catch (Exception e)
            {
                battleLogic.Surrender(battleLogic.WhoseTurn.PlayerNo);
                logger.LogError(e, "Error ending turn -> {ExceptionMessage}", e.Message);
                throw;
            }
            
        }

        return new BattleStatusUpdate
        {
            BattleStatus = battleLogic.WhoseTurn.PlayerNo == playerNo
                ? BattleStatus.YourTurn
                : BattleStatus.OpponentsTurn,
            PlayerUpdate = battleLogic.LatestPlayerUpdates[playerNo - 1]
        };
    }

    [HttpPost("endTurn/ai", Name = nameof(EndTurnAi))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> EndTurnAi([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        var startedMatch = await battleService.GetStartedMatch(playerRequest.MatchId);
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        
        if (battleLogic is {AiBattle: true, WhoseTurn.AiOpponent: true})
        {
            battleLogic.EndAiTurn();
        }

        return Ok();
    }

    [HttpPost("endTurn/player", Name = nameof(EndTurnPlayer))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> EndTurnPlayer([FromBody] PlayerRequestEndTurn playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        var startedMatch = await battleService.GetStartedMatch(playerRequest.MatchId);
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        var playerNo = battleLogic.GetBattlePlayerNo(playerRequest.Name);

        if (battleLogic.WhoseTurn.PlayerNo != playerNo) return Ok();
        try
        {
            battleLogic.EndPlayerTurn(playerNo, playerRequest.EndTurn);
        }
        catch (Exception e)
        {
            battleLogic.Surrender(playerNo);
            logger.LogError(e, "Error ending turn -> {ExceptionMessage}", e.Message);
            throw;
        }

        return Ok();
    }
    
    [HttpPost("targets", Name = nameof(Targets))]
    [ProducesResponseType(typeof(TargetsUpdate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TargetsUpdate>> Targets([FromBody] PlayerRequestGetTargets playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        var startedMatch = await battleService.GetStartedMatch(playerRequest.MatchId);
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        var playerNo = battleLogic.GetBattlePlayerNo(playerRequest.Name);

        if (battleLogic.WhoseTurn.PlayerNo == playerNo)
        {
            return battleLogic.GetTargets(playerNo, playerRequest.GetTargets);
        }

        return BadRequest();
    }
    
    [HttpPost("usable", Name = nameof(UsableAbilities))]
    [ProducesResponseType(typeof(UsableAbilitiesUpdate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UsableAbilitiesUpdate>> UsableAbilities([FromBody] PlayerRequestGetUsableAbilities playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        var startedMatch = await battleService.GetStartedMatch(playerRequest.MatchId);
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        var playerNo = battleLogic.GetBattlePlayerNo(playerRequest.Name);

        if (battleLogic.WhoseTurn.PlayerNo == playerNo)
        {
            return battleLogic.GetUsableAbilities(playerNo, playerRequest.GetUsableAbilities);
        }

        return BadRequest();
    }
    
    [HttpPost("exchange", Name = nameof(ExchangeEnergy))]
    [ProducesResponseType(typeof(ExchangeEnergyUpdate), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExchangeEnergyUpdate>> ExchangeEnergy([FromBody] PlayerRequestExchangeEnergy playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        var startedMatch = await battleService.GetStartedMatch(playerRequest.MatchId);
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        var playerNo = battleLogic.GetBattlePlayerNo(playerRequest.Name);

        if (battleLogic.WhoseTurn.PlayerNo == playerNo)
        {
            return battleLogic.ExchangeEnergy(playerNo, playerRequest.ExchangeEnergy);
        }

        return BadRequest();
    }
    
    [HttpPost("surrender", Name = nameof(Surrender))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Surrender([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        
        var startedMatch = await battleService.GetStartedMatch(playerRequest.MatchId);
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;
        var playerNo = battleLogic.GetBattlePlayerNo(playerRequest.Name);

        battleLogic.Surrender(playerNo);
        await matchHistoryService.AddBattleToMatchHistory(startedMatch);

        return Ok();
    }
    
    [HttpGet("timer/{matchId}", Name = nameof(BattleTimer))]
    [ProducesResponseType(typeof(TimerUpdate), StatusCodes.Status200OK)]
    public async Task<TimerUpdate> BattleTimer(string matchId)
    {
        var startedMatch = await battleService.GetStartedMatch(Guid.Parse(matchId));
        ArgumentNullException.ThrowIfNull(startedMatch);
        ArgumentNullException.ThrowIfNull(startedMatch.BattleLogic);
        var battleLogic = startedMatch.BattleLogic;

        var timeDifference = DateTime.UtcNow - battleLogic.TurnStartTime;

        return new TimerUpdate
        {
            UpdatedTimer = (int) (BattleConstants.TurnTimeInSeconds - timeDifference.TotalSeconds)
        };
    }
}
using Ashcrown.Remake.Api.Dtos.Inbound;
using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Draft;
using Ashcrown.Remake.Core.Draft.Dtos.Outbound;
using Ashcrown.Remake.Core.Draft.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ashcrown.Remake.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DraftController(IPlayerSessionService playerSessionService, 
    IDraftService draftService, IMatchHistoryService matchHistoryService) : ControllerBase
{
    
    [HttpPut("select/ban", Name = nameof(SelectBan))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SelectBan([FromBody] PlayerRequestSelectBanPickDraft playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        var draftMatch = await draftService.GetDraftMatch(playerRequest.MatchId);
        if (!draftMatch!.DraftLogic!.WhoseTurn.Equals(playerRequest.Name)
            || draftMatch.DraftLogic.DraftState != DraftState.Ban)
        {
            return BadRequest();
        }
        
        draftMatch.DraftLogic.SelectBan(playerRequest.ChampionName);
        return Ok();
    }
    
    [HttpPost("confirm/ban", Name = nameof(ConfirmBan))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ConfirmBan([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        var draftMatch = await draftService.GetDraftMatch(playerRequest.MatchId);
        if (!draftMatch!.DraftLogic!.WhoseTurn.Equals(playerRequest.Name)
            || draftMatch.DraftLogic.DraftState != DraftState.Ban)
        {
            return BadRequest();
        }
        
        draftMatch.DraftLogic.ConfirmBan();
        return Ok();
    }
    
    [HttpPut("select/pick", Name = nameof(SelectPick))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SelectPick([FromBody] PlayerRequestSelectBanPickDraft playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        var draftMatch = await draftService.GetDraftMatch(playerRequest.MatchId);
        if (!draftMatch!.DraftLogic!.WhoseTurn.Equals(playerRequest.Name)
            || draftMatch.DraftLogic.DraftState != DraftState.Pick)
        {
            return BadRequest();
        }
        
        draftMatch.DraftLogic.SelectPick(playerRequest.ChampionName);
        return Ok();
    }
    
    [HttpPost("confirm/pick", Name = nameof(ConfirmPick))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ConfirmPick([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        var draftMatch = await draftService.GetDraftMatch(playerRequest.MatchId);
        if (!draftMatch!.DraftLogic!.WhoseTurn.Equals(playerRequest.Name)
            || draftMatch.DraftLogic.DraftState != DraftState.Pick)
        {
            return BadRequest();
        }
        
        draftMatch.DraftLogic.ConfirmPick();
        return Ok();
    }
    
    [HttpPost("surrender", Name = nameof(SurrenderDraft))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SurrenderDraft([FromBody] PlayerRequestMatchId playerRequest)
    {
        await playerSessionService.ValidateProvidedSecret(playerRequest.Name, playerRequest.Secret);
        var draftMatch = await draftService.GetDraftMatch(playerRequest.MatchId);
        if (!draftMatch!.DraftLogic!.Players[0].Equals(playerRequest.Name)
            && !draftMatch.DraftLogic!.Players[1].Equals(playerRequest.Name)) return BadRequest();
        draftMatch.DraftLogic.Surrender(playerRequest.Name);
        await matchHistoryService.AddDraftToMatchToHistory(draftMatch);
        return Ok();

    }
    
    [HttpGet("timer/{matchId}", Name = nameof(DraftTimer))]
    [ProducesResponseType(typeof(TimerUpdate), StatusCodes.Status200OK)]
    public async Task<TimerUpdate> DraftTimer(string matchId)
    {
        var draftMatch = await draftService.GetDraftMatch(Guid.Parse(matchId));

        var timeDifference = DateTime.UtcNow - draftMatch!.DraftLogic!.TurnStartTime;
        
        return new TimerUpdate
        {
            UpdatedTimer = (int) ( draftMatch.DraftLogic.DraftState == DraftState.Ban ? DraftConstants.TimeToBanInSeconds 
                : DraftConstants.TimeToPickInSeconds - timeDifference.TotalSeconds)
        };
    }

    [HttpGet("update/{matchId}/{playerName}", Name = nameof(DraftUpdate))]
    [ProducesResponseType(typeof(DraftStatusUpdate), StatusCodes.Status200OK)]
    public async Task<DraftStatusUpdate> DraftUpdate(string matchId, string playerName)
    {
        var draftMatch = await draftService.GetDraftMatch(Guid.Parse(matchId));
        ArgumentNullException.ThrowIfNull(draftMatch);
        ArgumentNullException.ThrowIfNull(draftMatch.DraftLogic);
        var draftLogic = draftMatch.DraftLogic;

        var playerIndex = draftLogic.GetPlayerIndex(playerName);
        var opponentIndex = 1 - playerIndex;
        if (await playerSessionService.GetSessionAsync(draftLogic.Players[opponentIndex]) == null)
        {
            draftLogic.Surrender(draftLogic.Players[opponentIndex]);
            await matchHistoryService.AddDraftToMatchToHistory(draftMatch);
        }
        
        if (draftLogic.DraftState == DraftState.Ban 
            && draftLogic.TurnStartTime.AddSeconds(DraftConstants.TimeToBanInSeconds + 3) < DateTime.UtcNow)
        {
            draftLogic.ConfirmBan();
        }
        
        if (draftLogic.DraftState == DraftState.Pick 
            && draftLogic.TurnStartTime.AddSeconds(DraftConstants.TimeToPickInSeconds + 3) < DateTime.UtcNow)
        {
            draftLogic.ConfirmPick();
        }

        if (draftLogic.DraftState == DraftState.End)
        {
            await draftService.StartBattle(Guid.Parse(matchId));
        }
        
        return draftLogic.GetDraftUpdate(playerName);
    }
}
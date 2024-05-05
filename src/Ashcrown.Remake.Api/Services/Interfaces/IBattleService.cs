using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IBattleService
{
    bool AddAcceptedMatch(Guid matchId, FoundMatch foundMatch);
    bool IsAcceptedMatch(Guid matchId);
    Task StartAcceptedMatchBattle(Guid matchId, string playerName);
    Task<AcceptedMatchStatus> GetAcceptedMatchBattleStatus(Guid matchId);
}
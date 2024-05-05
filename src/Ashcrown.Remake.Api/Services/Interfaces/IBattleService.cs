using Ashcrown.Remake.Api.Models;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IBattleService
{
    bool AddAcceptedMatch(Guid matchId, FoundMatch foundMatch);
    bool IsAcceptedMatch(Guid matchId);
}
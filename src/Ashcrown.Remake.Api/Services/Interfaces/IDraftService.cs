using Ashcrown.Remake.Api.Models;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IDraftService
{
    bool AddAcceptedMatch(Guid matchId, FoundMatch foundMatch);
    bool IsAcceptedMatch(Guid matchId);
    
    Task<DraftMatch?> GetDraftMatch(Guid matchId);
    
    Task StartBattle(Guid matchId);
}
using Ashcrown.Remake.Api.Models;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IMatchHistoryService
{
    Task AddDraftToMatchToHistory(DraftMatch draftMatch);
    Task AddBattleToMatchHistory(StartedMatch startedMatch);
}
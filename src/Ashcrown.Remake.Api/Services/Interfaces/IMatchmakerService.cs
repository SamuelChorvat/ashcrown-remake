using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IMatchmakerService
{
    Task<bool> AddToMatchmaking(string playerName, FindMatchType matchType, string? opponentName);
    Task<bool> RemoveFromMatchMaking(string playerName);
    Task<FoundMatchResponse?> TryToMatchPlayer(string playerName);
    Task AcceptMatch(string playerName, Guid matchId);
    Task DeclineMatch(Guid matchId);
    Task<FoundMatchStatus> GetFoundMatchStatus(Guid matchId);
    Task<int> RemoveStaleFindMatches();
    Task<int> RemoveStaleFoundMatches();
}
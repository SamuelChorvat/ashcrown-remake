using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IMatchmakerService
{
    Task<bool> AddToMatchmaking(string playerName, FindMatchType matchType, string? opponentName);
    Task<bool> RemoveFromMatchMaking(string playerName);
    Task<FoundMatchResponse?> TryToMatchPlayer(string playerName);
    Task AcceptMatch(string playerName, string matchId);
    Task DeclineMatch(string matchId);
    Task<FoundMatchStatus> GetFoundMatchStatus(string matchId);
    Task<int> RemoveStaleFoundMatches();
}
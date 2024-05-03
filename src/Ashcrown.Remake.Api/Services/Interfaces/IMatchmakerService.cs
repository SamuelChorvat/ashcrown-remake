namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IMatchmakerService
{
    Task<bool> AddToMatchmaking(string playerName, MatchType matchType, string? opponentName);
    Task<bool> RemoveFromMatchMaking(string playerName);
}
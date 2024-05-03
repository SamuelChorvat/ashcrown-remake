using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Services.Interfaces;

public interface IMatchmakerService
{
    Task<bool> AddToMatchmaking(string playerName, FindMatchType matchType, string? opponentName);
    Task<bool> RemoveFromMatchMaking(string playerName);
}
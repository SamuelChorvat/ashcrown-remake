using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.Services;

public class MatchmakerService : IMatchmakerService
{
    private readonly ConcurrentDictionary<string, FindMatch> _findMatches = [];
    
    public Task<bool> AddToMatchmaking(string playerName, MatchType matchType, string? opponentName)
    {
        return Task.FromResult(_findMatches.TryAdd(playerName, new FindMatch
        {
            MatchType = matchType,
            PrivateOpponentName = opponentName
        }));
    }

    public Task<bool> RemoveFromMatchMaking(string playerName)
    {
        return Task.FromResult(_findMatches.TryRemove(playerName, out _));
    }
}

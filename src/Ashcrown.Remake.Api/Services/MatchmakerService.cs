using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Models.Enums;
using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.Services;

public class MatchmakerService : IMatchmakerService
{
    private readonly ConcurrentDictionary<string, FindMatch> _findMatches = [];
    private readonly ConcurrentDictionary<string, FindMatch> _acceptedMatches = []; //TODO
    
    public Task<bool> AddToMatchmaking(string playerName, FindMatchType matchType, string? opponentName)
    {
        if (matchType is FindMatchType.BlindPrivate or FindMatchType.DraftPrivate 
            && string.IsNullOrEmpty(opponentName))
        {
            return Task.FromResult(false);
        }

        _findMatches.TryRemove(playerName, out _);
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

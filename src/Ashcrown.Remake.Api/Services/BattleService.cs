using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.Services;

public class BattleService : IBattleService
{
    private readonly ConcurrentDictionary<Guid, AcceptedMatch> _acceptedMatches = [];
    
    public bool AddAcceptedMatch(Guid matchId, FoundMatch foundMatch)
    {
        return _acceptedMatches.TryAdd(matchId, new AcceptedMatch
        {
            FoundMatch = foundMatch
        });
    }

    public bool IsAcceptedMatch(Guid matchId)
    {
        return _acceptedMatches.ContainsKey(matchId);
    }
}
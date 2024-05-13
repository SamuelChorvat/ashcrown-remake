using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Models.Enums;
using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.Services;

public class BattleService : IBattleService
{
    private readonly ConcurrentDictionary<Guid, AcceptedMatch> _acceptedMatches = [];
    private readonly ConcurrentDictionary<Guid, StartedMatch> _startedMatches = [];
    
    public bool AddAcceptedMatch(Guid matchId, FoundMatch foundMatch, DraftMatch? draftMatch = null)
    {
        lock (this)
        {
            return _acceptedMatches.TryAdd(matchId, new AcceptedMatch
            {
                FoundMatch = foundMatch,
                DraftMatch = draftMatch,
                PlayerBattleStarted = [false, foundMatch.FindMatchType == FindMatchType.BlindAi]
            });
        }
    }

    public bool IsAcceptedMatchAlreadyAdded(Guid matchId)
    {
        lock (this)
        {
            return _acceptedMatches.ContainsKey(matchId);
        }
    }

    public Task StartAcceptedMatchBattle(Guid matchId, string playerName)
    {
        UpdateAcceptedMatch(matchId, acceptedMatch => 
            acceptedMatch.PlayerBattleStarted[acceptedMatch.FoundMatch.PlayerNames[0].Equals(playerName) ? 0 : 1] = true);
        return Task.CompletedTask;
    }

    public Task<AcceptedMatchStatus> GetAcceptedMatchBattleStatus(Guid matchId)
    {
        lock (this)
        {
            if (_startedMatches.ContainsKey(matchId))
            {
                return Task.FromResult(AcceptedMatchStatus.Started);
            }

            _acceptedMatches.TryGetValue(matchId, out var acceptedMatch);
            if (acceptedMatch is null)
            {
                return Task.FromResult(AcceptedMatchStatus.Cancelled);
            }

            if (acceptedMatch.PlayerBattleStarted.Count(x => x) == 2)
            {
                _acceptedMatches.TryRemove(matchId, out _);
                _startedMatches.TryAdd(matchId, new StartedMatch(acceptedMatch));
                return Task.FromResult(AcceptedMatchStatus.Started);
            }
            
            if (acceptedMatch.CreatedAt.AddSeconds(3) <
                DateTime.UtcNow)
            {
                _acceptedMatches.TryRemove(matchId, out _);
                return Task.FromResult(AcceptedMatchStatus.Cancelled);
            }
            
            return Task.FromResult(AcceptedMatchStatus.Pending);
        }
    }

    public Task<StartedMatch?> GetStartedMatch(Guid matchId)
    {
        _startedMatches.TryGetValue(matchId, out var startedMatch);
        return Task.FromResult(startedMatch);
    }

    public Task<int> ClearStaleMatches(int staleMatchLimitInMinutes)
    {
        lock (this)
        {
            var cutoffTime = DateTime.UtcNow.AddMinutes(-staleMatchLimitInMinutes);
            var keysToRemoveAccepted = _acceptedMatches.Where(pair => pair.Value.CreatedAt < cutoffTime)
                .Select(pair => pair.Key)
                .ToList();
            var keysToRemoveStarted = _startedMatches.Where(pair => pair.Value.CreatedAt < cutoffTime)
                .Select(pair => pair.Key)
                .ToList();

            var matchesRemoved = keysToRemoveAccepted.Count(key => _acceptedMatches.TryRemove(key, out _));
            matchesRemoved += keysToRemoveStarted.Count(key => _startedMatches.TryRemove(key, out _));

            return Task.FromResult(matchesRemoved);
        }
    }

    private void UpdateAcceptedMatch(Guid matchId, Action<AcceptedMatch> updateAction)
    {
        lock (this)
        {
            _acceptedMatches.AddOrUpdate(matchId,
                key => throw new KeyNotFoundException($"No accepted match with matchId {key}"), 
                (_, existingSession) =>
                {
                    updateAction(existingSession);
                    return existingSession; 
                });
        }
    }
}
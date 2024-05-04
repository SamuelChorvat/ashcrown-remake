using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Dtos.Outbound;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Models.Enums;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Champion;
// ReSharper disable InconsistentlySynchronizedField

namespace Ashcrown.Remake.Api.Services;

public class MatchmakerService(IPlayerSessionService playerSessionService) : IMatchmakerService
{
    private readonly ConcurrentDictionary<string, FindMatch> _findMatches = [];
    private readonly ConcurrentDictionary<Guid, FoundMatch> _foundMatches = [];
    
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

    public Task<FoundMatchResponse?> TryToMatchPlayer(string playerName)
    {
        //TODO Write test for this
        //TODO Handle FindMatches for player that went offline
        lock (this)
        {
            var existingFoundMatch = _foundMatches.FirstOrDefault(pair => 
                pair.Value.PlayerNames[0] == playerName 
                || pair.Value.PlayerNames[1] == playerName).Value;

            if (existingFoundMatch is not null)
            {
                return Task.FromResult(BuildFoundMatchResponse(playerName, existingFoundMatch))!;
            }
            
            _findMatches.TryGetValue(playerName, out var playerFindMatch);
            ArgumentNullException.ThrowIfNull(playerFindMatch);
            var playerSession = playerSessionService.GetSession(playerName);
            ArgumentNullException.ThrowIfNull(playerSession);
            
            FoundMatchResponse? matchFound = null;
            switch (playerFindMatch.MatchType)
            {
                case FindMatchType.BlindAi:
                    var foundMatch = BuildFoundMatch(playerFindMatch.MatchType, playerSession);
                    _findMatches.TryRemove(playerName, out _);
                    _foundMatches.TryAdd(foundMatch.MatchId, foundMatch);
                    matchFound = BuildFoundMatchResponse(playerName, foundMatch);
                    break;
                case FindMatchType.BlindPrivate:
                case FindMatchType.DraftPrivate:
                    var opponentFindMatchPairPrivate = _findMatches.FirstOrDefault(pair =>
                        pair.Value.MatchType == playerFindMatch.MatchType 
                        && pair.Value.PrivateOpponentName != null 
                        && pair.Value.PrivateOpponentName.Equals(playerName));
                    if (opponentFindMatchPairPrivate.Value is not null)
                    {
                        matchFound = NonAiFoundMatchHelper(playerName, playerFindMatch, playerSession, opponentFindMatchPairPrivate);
                    }
                    break;
                case FindMatchType.BlindPublic:
                case FindMatchType.DraftPublic:
                    var opponentFindMatchPairPublic = _findMatches.FirstOrDefault(pair => 
                        pair.Value.MatchType == playerFindMatch.MatchType);
                    if (opponentFindMatchPairPublic.Value is not null)
                    {
                        matchFound = NonAiFoundMatchHelper(playerName, playerFindMatch, playerSession, opponentFindMatchPairPublic);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(playerFindMatch.MatchType.ToString());
            }
            
            return Task.FromResult(matchFound);
        }
    }

    public Task AcceptMatch(string playerName, string matchId)
    {
        UpdateFoundMatch(Guid.Parse(matchId),
            foundMatch => foundMatch.PlayerAccepted[foundMatch.PlayerNames[0].Equals(playerName) ? 0 : 1] = true);
            
        return Task.CompletedTask;
    }

    public Task DeclineMatch(string matchId)
    {
        UpdateFoundMatch(Guid.Parse(matchId),
            foundMatch => foundMatch.MatchCancelled = true);
            
        return Task.CompletedTask;
    }

    public Task<FoundMatchStatus> GetFoundMatchStatus(string matchId)
    {
        //TODO Check BattleService first to see if it has been moved already, return confirmed if yes
        
        _foundMatches.TryGetValue(Guid.Parse(matchId), out var foundMatch);
        if (foundMatch is null || foundMatch.MatchCancelled)
        {
            _foundMatches.TryRemove(Guid.Parse(matchId), out _);
            return Task.FromResult(FoundMatchStatus.Cancelled);
        }

        if (foundMatch.PlayerAccepted.Count(x => x) == 2)
        {
            //TODO Call BattleService and move the match there and remove it from here
            return Task.FromResult(FoundMatchStatus.Confirmed);
        }

        if (foundMatch.MatchFoundTime.AddSeconds(AshcrownApiConstants.TimeToAcceptMatchFoundSeconds + 1) <
            DateTime.UtcNow)
        {
            _foundMatches.TryRemove(Guid.Parse(matchId), out _);
            return Task.FromResult(FoundMatchStatus.Cancelled);
        }

        return Task.FromResult(FoundMatchStatus.Pending);
    }

    public Task<int> RemoveStaleFoundMatches()
    {
        lock (this)
        {
            var keysToRemove = _foundMatches.Where(pair => 
                    pair.Value.MatchFoundTime.AddSeconds(AshcrownApiConstants.TimeToAcceptMatchFoundSeconds + 2) < DateTime.UtcNow)
                .Select(pair => pair.Key)
                .ToList();

            var matchedFoundRemoved = keysToRemove.Count(key => _foundMatches.TryRemove(key, out _));

            return Task.FromResult(matchedFoundRemoved);
        }
    }

    private void UpdateFoundMatch(Guid matchId, Action<FoundMatch> updateAction)
    {
        lock (this)
        {
            _foundMatches.AddOrUpdate(matchId,
                key => throw new KeyNotFoundException($"No found match with matchId {key}"), 
                (_, existingSession) =>
                {
                    updateAction(existingSession);
                    return existingSession; 
                });
        }
    }

    private FoundMatchResponse NonAiFoundMatchHelper(string playerName, FindMatch playerFindMatch, 
        PlayerSession playerSession, KeyValuePair<string, FindMatch> opponentFindMatchPair)
    {
        var opponentSession = playerSessionService.GetSession(opponentFindMatchPair.Key);
        ArgumentNullException.ThrowIfNull(opponentSession);
                        
        var foundMatch = BuildFoundMatch(playerFindMatch.MatchType, playerSession, opponentSession);
        _findMatches.TryRemove(playerName, out _);
        _findMatches.TryRemove(opponentSession.PlayerName, out _);
        _foundMatches.TryAdd(foundMatch.MatchId, foundMatch);
        return BuildFoundMatchResponse(playerName, foundMatch);
    }

    private static FoundMatch BuildFoundMatch(FindMatchType findMatchType, PlayerSession player1, PlayerSession? player2 = null)
    {
        return new FoundMatch
        {
            FindMatchType = findMatchType,
            PlayerAccepted = [false, player2 == null],
            PlayerNames = [player1.PlayerName, player2 != null ? player2.PlayerName : "AshcrownNET"],
            PlayerIcons = [player1.IconName, player2 != null ? player2.IconName : "AshcrownNET"],
            PlayerCrowns = [player1.CrownName, player2 != null ? player2.CrownName :"Ashcrown"],
            PlayerBlindChampions = [player1.BlindChampions, 
                player2 != null ? player2.BlindChampions : ChampionConstants.GetRandomChampionNames(3)]
        };
    }

    private static FoundMatchResponse BuildFoundMatchResponse(string playerName, FoundMatch foundMatch)
    {
        var opponentIndex = foundMatch.PlayerNames[0].Equals(playerName) ? 1 : 0;
        
        return new FoundMatchResponse
        {
            MatchId = foundMatch.MatchId,
            MatchType = foundMatch.FindMatchType,
            OpponentName = foundMatch.PlayerNames[opponentIndex],
            OpponentIcon = foundMatch.PlayerIcons[opponentIndex],
            OpponentCrown = foundMatch.PlayerCrowns[opponentIndex],
            OpponentChampions = foundMatch.PlayerBlindChampions[opponentIndex]
        };
    }
}

﻿using System.Collections.Concurrent;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;

namespace Ashcrown.Remake.Api.Services;

public class DraftService(IBattleService battleService) : IDraftService
{
    private readonly ConcurrentDictionary<Guid, DraftMatch> _draftMatches = [];
    
    public bool AddAcceptedMatch(Guid matchId, FoundMatch foundMatch)
    {
        return _draftMatches.TryAdd(matchId, new DraftMatch(foundMatch));
    }

    public bool IsAcceptedMatch(Guid matchId)
    {
        return _draftMatches.ContainsKey(matchId);
    }

    public Task<DraftMatch?> GetDraftMatch(Guid matchId)
    {
        _draftMatches.TryGetValue(matchId, out var draftMatch);
        return Task.FromResult(draftMatch);
    }

    public Task StartBattle(Guid matchId)
    {
        _draftMatches.TryGetValue(matchId, out var draftMatch);
        if (draftMatch == null || battleService.IsAcceptedMatch(matchId)) return Task.CompletedTask;
        var playerIndex = draftMatch.DraftLogic!.Players[0].Equals(draftMatch.FoundMatch.PlayerNames[0]) ? 0 : 1;
        draftMatch.FoundMatch.PlayerBlindChampions[playerIndex] =
        [
            draftMatch.DraftLogic!.PickedChampions[0, 0],
            draftMatch.DraftLogic!.PickedChampions[0, 1],
            draftMatch.DraftLogic!.PickedChampions[0, 2]
        ];
        draftMatch.FoundMatch.PlayerBlindChampions[1 - playerIndex] =
        [
            draftMatch.DraftLogic!.PickedChampions[1, 0],
            draftMatch.DraftLogic!.PickedChampions[1, 1],
            draftMatch.DraftLogic!.PickedChampions[1, 2]
        ];
        battleService.AddAcceptedMatch(matchId, draftMatch.FoundMatch, draftMatch);
        return Task.CompletedTask;
    }
}
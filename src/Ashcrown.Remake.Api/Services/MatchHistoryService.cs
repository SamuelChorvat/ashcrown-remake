using Ashcrown.Remake.Api.Extensions;
using Ashcrown.Remake.Api.Models;
using Ashcrown.Remake.Api.Services.Interfaces;
using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Draft.Enums;

namespace Ashcrown.Remake.Api.Services;

public class MatchHistoryService(
    IPlayerSessionService playerSessionService) : IMatchHistoryService
{
    public async Task AddDraftToMatchToHistory(DraftMatch draftMatch)
    {
        var draftLogic = draftMatch.DraftLogic!;
        var winnerIndex = draftLogic.DraftStatuses[0] == DraftStatus.Victory ? 0 : 1;
        var winnerFoundMatchIndex = draftMatch.FoundMatch.PlayerNames[0].Equals(draftLogic.Players[winnerIndex])
            ? 0
            : 1;
        var matchRecord = new MatchRecord
        {
            MatchId = draftMatch.FoundMatch.MatchId,
            WinnerName = draftLogic.Players[winnerIndex],
            WinnerIcon = draftMatch.FoundMatch.PlayerIcons[winnerFoundMatchIndex],
            WinnerBans =
            [
                string.IsNullOrEmpty(draftLogic.BannedChampions[winnerIndex, 0])
                    ? "No Ban"
                    : draftLogic.BannedChampions[winnerIndex, 0],
                string.IsNullOrEmpty(draftLogic.BannedChampions[winnerIndex, 1]) 
                    ? "No Ban" : draftLogic.BannedChampions[winnerIndex, 1],
                string.IsNullOrEmpty(draftLogic.BannedChampions[winnerIndex, 2]) 
                    ? "No Ban" : draftLogic.BannedChampions[winnerIndex, 2]
            ],
            WinnerPicks =
            [
                string.IsNullOrEmpty(draftLogic.PickedChampions[winnerIndex, 0])
                    ? "No Pick"
                    : draftLogic.PickedChampions[winnerIndex, 0],
                string.IsNullOrEmpty(draftLogic.PickedChampions[winnerIndex, 1]) 
                    ? "No Pick" : draftLogic.PickedChampions[winnerIndex, 1],
                string.IsNullOrEmpty(draftLogic.PickedChampions[winnerIndex, 2]) 
                    ? "No Pick" : draftLogic.PickedChampions[winnerIndex, 2]
            ],
            LoserName = draftLogic.Players[1 - winnerIndex],
            LoserIcon = draftMatch.FoundMatch.PlayerIcons[1 - winnerFoundMatchIndex],
            LoserBans =
            [
                string.IsNullOrEmpty(draftLogic.BannedChampions[1 -winnerIndex, 0])
                    ? "No Ban"
                    : draftLogic.BannedChampions[1 - winnerIndex, 0],
                string.IsNullOrEmpty(draftLogic.BannedChampions[1 -winnerIndex, 1]) 
                    ? "No Ban" : draftLogic.BannedChampions[1 -winnerIndex, 1],
                string.IsNullOrEmpty(draftLogic.BannedChampions[1 -winnerIndex, 2]) 
                    ? "No Ban" : draftLogic.BannedChampions[1 - winnerIndex, 2]
            ],
            LoserPicks =
            [
                string.IsNullOrEmpty(draftLogic.PickedChampions[1 - winnerIndex, 0])
                    ? "No Pick"
                    : draftLogic.PickedChampions[1 - winnerIndex, 0],
                string.IsNullOrEmpty(draftLogic.PickedChampions[1 - winnerIndex, 1]) 
                    ? "No Pick" : draftLogic.PickedChampions[1 - winnerIndex, 1],
                string.IsNullOrEmpty(draftLogic.PickedChampions[1 - winnerIndex, 2]) 
                    ? "No Pick" : draftLogic.PickedChampions[1- winnerIndex, 2]
            ],
            MatchType = draftMatch.FoundMatch.FindMatchType,
            MatchStart = draftMatch.CreatedAt,
            MatchDurationSeconds = (int) (DateTime.UtcNow - draftMatch.CreatedAt).TotalSeconds
        };

        foreach (var playerName in draftLogic.Players)
        {
            var playerSession = await playerSessionService.GetSessionAsync(playerName);
            if (playerSession == null 
                || playerSession.MatchHistory.Exists(x => x.MatchId == matchRecord.MatchId)) continue;
            await playerSessionService
                .UpdateSessionNoSecret(playerName, x => x.MatchHistory.Add(matchRecord));
        }
    }

    public async Task AddBattleToMatchHistory(StartedMatch startedMatch)
    {
        var draftLogic = startedMatch.AcceptedMatch.DraftMatch?.DraftLogic;
        var battleLogic = startedMatch.BattleLogic!;
        var winnerIndex = battleLogic.BattleEndedUpdates![0] == BattleStatus.Victory ? 0 : 1;
        var winnerFoundMatchIndex = startedMatch.AcceptedMatch.FoundMatch.PlayerNames[0]
            .Equals(battleLogic.BattlePlayers[winnerIndex].PlayerName)
            ? 0
            : 1;
        var matchRecord = new MatchRecord
        {
            MatchId = startedMatch.AcceptedMatch.FoundMatch.MatchId,
            WinnerName = battleLogic.BattlePlayers[winnerIndex].PlayerName,
            WinnerIcon = startedMatch.AcceptedMatch.FoundMatch.PlayerIcons[winnerFoundMatchIndex],
            WinnerPicks =
            [
                startedMatch.AcceptedMatch.FoundMatch.PlayerBlindChampions[winnerFoundMatchIndex][0],
                startedMatch.AcceptedMatch.FoundMatch.PlayerBlindChampions[winnerFoundMatchIndex][1],
                startedMatch.AcceptedMatch.FoundMatch.PlayerBlindChampions[winnerFoundMatchIndex][2]
            ],
            LoserName = battleLogic.BattlePlayers[1 - winnerIndex].PlayerName,
            LoserIcon = startedMatch.AcceptedMatch.FoundMatch.PlayerIcons[1 - winnerFoundMatchIndex],
            LoserPicks =
            [
                startedMatch.AcceptedMatch.FoundMatch.PlayerBlindChampions[1 - winnerFoundMatchIndex][0],
                startedMatch.AcceptedMatch.FoundMatch.PlayerBlindChampions[1 - winnerFoundMatchIndex][1],
                startedMatch.AcceptedMatch.FoundMatch.PlayerBlindChampions[1 - winnerFoundMatchIndex][2]
            ],
            MatchType = startedMatch.AcceptedMatch.FoundMatch.FindMatchType,
            MatchStart = startedMatch.AcceptedMatch.FoundMatch.FindMatchType.IsDraft() 
                ? startedMatch.AcceptedMatch.DraftMatch!.CreatedAt
                : startedMatch.CreatedAt,
            MatchDurationSeconds = (int) (DateTime.UtcNow - (startedMatch.AcceptedMatch.FoundMatch.FindMatchType.IsDraft() 
                ? startedMatch.AcceptedMatch.DraftMatch!.CreatedAt
                : startedMatch.CreatedAt)).TotalSeconds
        };

        if (matchRecord.MatchType.IsDraft())
        {
            matchRecord.WinnerBans =
            [
                string.IsNullOrEmpty(draftLogic!.BannedChampions[winnerIndex, 0])
                    ? "No Ban"
                    : draftLogic.BannedChampions[winnerIndex, 0],
                string.IsNullOrEmpty(draftLogic.BannedChampions[winnerIndex, 1])
                    ? "No Ban"
                    : draftLogic.BannedChampions[winnerIndex, 1],
                string.IsNullOrEmpty(draftLogic.BannedChampions[winnerIndex, 2])
                    ? "No Ban"
                    : draftLogic.BannedChampions[winnerIndex, 2]
            ];
            matchRecord.LoserBans =
            [
                string.IsNullOrEmpty(draftLogic.BannedChampions[1 - winnerIndex, 0])
                    ? "No Ban"
                    : draftLogic.BannedChampions[1 - winnerIndex, 0],
                string.IsNullOrEmpty(draftLogic.BannedChampions[1 - winnerIndex, 1])
                    ? "No Ban"
                    : draftLogic.BannedChampions[1 - winnerIndex, 1],
                string.IsNullOrEmpty(draftLogic.BannedChampions[1 - winnerIndex, 2])
                    ? "No Ban"
                    : draftLogic.BannedChampions[1 - winnerIndex, 2]
            ];
        }
        
        foreach (var playerName in startedMatch.AcceptedMatch.FoundMatch.PlayerNames)
        {
            var playerSession = await playerSessionService.GetSessionAsync(playerName);
            if (playerSession == null 
                || playerSession.MatchHistory.Exists(x => x.MatchId == matchRecord.MatchId)) continue;
            await playerSessionService
                .UpdateSessionNoSecret(playerName, x => x.MatchHistory.Add(matchRecord));
        }
    }
}
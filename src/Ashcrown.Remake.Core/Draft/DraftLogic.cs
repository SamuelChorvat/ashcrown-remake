using Ashcrown.Remake.Core.Champion;
using Ashcrown.Remake.Core.Draft.Dtos.Outbound;
using Ashcrown.Remake.Core.Draft.Enums;

namespace Ashcrown.Remake.Core.Draft;

public class DraftLogic
{
    public const int TimeToBanInSeconds = 15;
    public const int TimeToPickInSeconds = 20;
    public const int BattleReadyTimerDurationInSeconds = 5;
    
    public int[] CurrentBanNo { get; set; } = [0, 0];
    public int[] CurrentPickNo { get; set; } = [0, 0];
    public string?[,] BannedChampions { get; } = new string[2,3];
    public string?[,] PickedChampions { get; } = new string[2,3];

    public DraftState DraftState { get; set; } = DraftState.Ban;
    public DraftStatus[] DraftStatuses { get; set; } = new DraftStatus[2];
    public string[] Players { get; init; }
    public DateTime TurnStartTime { get; set;} = DateTime.UtcNow;
    public DateTime EndDraftTime { get; set; }
    public string WhoseTurn { get; set; }

    private bool IsChampionBanned(string championName) {
        if (championName.Equals("No Ban")) {
            return false;
        }

        return BannedChampions.OfType<string>()
            .Any(bannedChampion => bannedChampion.Equals(championName));
    }

    private bool IsChampionPicked(string championName) {
        return PickedChampions.OfType<string>()
            .Any(pickedChampion => pickedChampion.Equals(championName));
    }
    
    public void SelectBan(string championName)
    {
        if (IsChampionBanned(championName))
        {
            throw new Exception("Champion already banned!"); 
        }
        
        if (!championName.Equals("No Ban") && !ChampionConstants.AllChampionsNames.Contains(championName))
        {
            throw new Exception("Non existent champion!");
        }

        BannedChampions[GetWhoseTurnIndex(), CurrentBanNo[GetWhoseTurnIndex()]] = championName;
    }

    public void ConfirmBan()
    {
        BannedChampions[GetWhoseTurnIndex(), CurrentBanNo[GetWhoseTurnIndex()]] ??= "No Ban";
        if (GetWhoseTurnIndex() == 1 && CurrentBanNo[GetWhoseTurnIndex()] == 2)
        {
            DraftState = DraftState.Pick;
            WhoseTurn = Players[1 - GetWhoseTurnIndex()];
            TurnStartTime = DateTime.UtcNow;
            DraftStatuses[GetWhoseTurnIndex()] = DraftStatus.YourPick;
            DraftStatuses[1 - GetWhoseTurnIndex()] = DraftStatus.OpponentsPick;
            return;
        }
        CurrentBanNo[GetWhoseTurnIndex()] += 1;
        WhoseTurn = Players[1 - GetWhoseTurnIndex()];
        TurnStartTime = DateTime.UtcNow;
        DraftStatuses[GetWhoseTurnIndex()] = DraftStatus.YourBan;
        DraftStatuses[1 - GetWhoseTurnIndex()] = DraftStatus.OpponentsBan;
    }
    
    public void SelectPick(string championName)
    {
        if (IsChampionPicked(championName) || IsChampionBanned(championName))
        {
            throw new Exception("Champion already picked or banned!"); 
        }
        
        if (!ChampionConstants.AllChampionsNames.Contains(championName))
        {
            throw new Exception("Non existent champion!");
        }

        PickedChampions[GetWhoseTurnIndex(), CurrentPickNo[GetWhoseTurnIndex()]] = championName;
    }
    
    public void ConfirmPick()
    {
        if (PickedChampions[GetWhoseTurnIndex(), CurrentPickNo[GetWhoseTurnIndex()]] == null)
        {
            Surrender(WhoseTurn);
            return;
        }
        
        if (GetWhoseTurnIndex() == 1 && CurrentPickNo[GetWhoseTurnIndex()] == 2)
        {
            DraftState = DraftState.End;
            DraftStatuses = [DraftStatus.BattleStarting, DraftStatus.BattleStarting];
            EndDraftTime = DateTime.UtcNow;
            return;
        }
        CurrentPickNo[GetWhoseTurnIndex()] += 1;
        WhoseTurn = Players[1 - GetWhoseTurnIndex()];
        TurnStartTime = DateTime.UtcNow;
        DraftStatuses[GetWhoseTurnIndex()] = DraftStatus.YourPick;
        DraftStatuses[1 - GetWhoseTurnIndex()] = DraftStatus.OpponentsPick;
    }

    public void Surrender(string playerName)
    {
        var playerIndex = Players[0].Equals(playerName) ? 0 : 1;
        EndDraftTime = DateTime.UtcNow;
        DraftState = DraftState.End;
        DraftStatuses[playerIndex] = DraftStatus.Defeat;
        DraftStatuses[1 - playerIndex] = DraftStatus.Victory;

    }

    private int GetWhoseTurnIndex()
    {
        return Players[0].Equals(WhoseTurn) ? 0 : 1;
    }

    public DraftStatusUpdate GetDraftUpdate(string playerName)
    {
        var playerIndex = Players[0].Equals(playerName) ? 0 : 1;
        return new DraftStatusUpdate
        {
            DraftStatus = DraftStatuses[playerIndex],
            YourBanNo = CurrentBanNo[playerIndex],
            OpponentBanNo = CurrentBanNo[1 - playerIndex],
            YourPickNo = CurrentPickNo[playerIndex],
            OpponentPickNo = CurrentPickNo[1 - playerIndex],
            YourBans =
            [
                BannedChampions[playerIndex, 0],
                BannedChampions[playerIndex, 1],
                BannedChampions[playerIndex, 2]
            ],
            OpponentBans =
            [
                BannedChampions[1 - playerIndex, 0],
                BannedChampions[1 - playerIndex, 1],
                BannedChampions[1 - playerIndex, 2]
            ],
            YourPicks = 
            [
                PickedChampions[playerIndex, 0],
                PickedChampions[playerIndex, 1],
                PickedChampions[playerIndex, 2]
            ],
            OpponentPicks = 
            [
                PickedChampions[1 -playerIndex, 0],
                PickedChampions[1 -playerIndex, 1],
                PickedChampions[1- playerIndex, 2]
            ],
        };
    }
}
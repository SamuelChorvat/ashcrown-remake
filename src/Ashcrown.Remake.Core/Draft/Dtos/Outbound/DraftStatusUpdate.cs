using Ashcrown.Remake.Core.Draft.Enums;

namespace Ashcrown.Remake.Core.Draft.Dtos.Outbound;

public class DraftStatusUpdate
{
    public int TimeToBan { get; init; } = DraftConstants.TimeToBanInSeconds;
    public int TimeToPick { get; init; } = DraftConstants.TimeToPickInSeconds;
    public int BattleStartingTime { get; } = DraftConstants.BattleReadyTimerDurationInSeconds;
    public DraftStatus DraftStatus { get; init; }
    public required DateTime TurnStartTime { get; init; }
    public required int YourBanNo { get; init; }
    public required int OpponentBanNo { get; init; }
    public required int YourPickNo { get; init; }
    public required int OpponentPickNo { get; init; }
    public required string[] YourBans { get; init; }
    public required string[] OpponentBans { get; init; }
    public required string[] YourPicks { get; init; }
    public required string[] OpponentPicks { get; init; }
}
using Ashcrown.Remake.Core.Draft.Enums;

namespace Ashcrown.Remake.Core.Draft.Dtos.Outbound;

public class DraftStatusUpdate
{
    public int TimeToBan { get; set; } = DraftConstants.TimeToBanInSeconds;
    public int TimeToPick { get; set; } = DraftConstants.TimeToPickInSeconds;
    public int BattleStartingTime { get; } = DraftConstants.BattleReadyTimerDurationInSeconds;
    public DraftStatus DraftStatus { get; set; }
    public required DateTime TurnStartTime { get; set; }
    public required int YourBanNo { get; set; }
    public required int OpponentBanNo { get; set; }
    public required int YourPickNo { get; set; }
    public required int OpponentPickNo { get; set; }
    public required string[] YourBans { get; set; }
    public required string[] OpponentBans { get; set; }
    public required string[] YourPicks { get; set; }
    public required string[] OpponentPicks { get; set; }
}
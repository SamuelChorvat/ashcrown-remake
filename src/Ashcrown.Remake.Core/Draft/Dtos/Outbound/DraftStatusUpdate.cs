using Ashcrown.Remake.Core.Draft.Enums;

namespace Ashcrown.Remake.Core.Draft.Dtos.Outbound;

public class DraftStatusUpdate
{
    public int TimeToBan { get; set; } = DraftLogic.TimeToBanInSeconds;
    public int TimeToPick { get; set; } = DraftLogic.TimeToPickInSeconds;
    public int BattleStartingTime { get; } = DraftLogic.BattleReadyTimerDurationInSeconds;
    public DraftStatus DraftStatus { get; set; }
    public required int YourBanNo { get; set; }
    public required int OpponentBanNo { get; set; }
    public required int YourPickNo { get; set; }
    public required int OpponentPickNo { get; set; }
    public required string[] YourBans { get; set; }
    public required string[] OpponentBans { get; set; }
    public required string[] YourPicks { get; set; }
    public required string[] OpponentPicks { get; set; }
}
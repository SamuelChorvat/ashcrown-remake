using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Models;

public class FoundMatch
{
    public Guid MatchId { get; init; } =  Guid.NewGuid();
    public DateTime MatchFoundTime { get; init; } = DateTime.UtcNow;
    public required FindMatchType FindMatchType { get; init; }
    public bool[] PlayerAccepted { get; init; } = [false, false];
    public string[] PlayerNames { get; init;} = new string[2];
    public string[] PlayerIcons { get; init;} = new string[2];
    public string[] PlayerCrowns { get; init;} = new string[2];
    public string[][] PlayerBlindChampions { get; init;} = new string[2][];
    public bool MatchCancelled { get; set; } = false;
}
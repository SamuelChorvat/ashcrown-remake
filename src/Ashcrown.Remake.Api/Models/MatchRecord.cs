using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Models;

public class MatchRecord
{
    public required Guid MatchId { get; set; }
    public required string WinnerName { get; set; }
    public required string WinnerIcon { get; set; }
    public string?[] WinnerBans { get; set; } = ["No Ban", "No Ban", "No Ban"];
    public string?[] WinnerPicks { get; set; } = ["No Pick", "No Pick", "No Pick"];
    public required string LoserName { get; set; }
    public required string LoserIcon { get; set; }
    public string?[] LoserBans { get; set; } = ["No Ban", "No Ban", "No Ban"];
    public string?[] LoserPicks { get; set; } = ["No Pick", "No Pick", "No Pick"];
    public FindMatchType MatchType { get; set; }
    public DateTime MatchStart { get; set; }
    public int MatchDurationSeconds { get; set; }
}
namespace Ashcrown.Remake.Api.Models;

public class AcceptedMatch
{
    public required FoundMatch FoundMatch { get; init; }
    public bool[] PlayerBattleStarted { get; init; } = [false, false];
    public DateTime CreatedAt = DateTime.UtcNow;
}
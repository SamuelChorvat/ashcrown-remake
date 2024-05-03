namespace Ashcrown.Remake.Api.Models;

public class FindMatch
{
    public required MatchType MatchType { get; init; }
    public string? PrivateOpponentName { get; init; }
}
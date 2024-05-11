using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Models;

public class FindMatch
{
    public required FindMatchType MatchType { get; init; }
    public string? PrivateOpponentName { get; init; }
}
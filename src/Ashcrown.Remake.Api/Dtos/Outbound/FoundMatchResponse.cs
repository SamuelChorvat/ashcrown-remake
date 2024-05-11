using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class FoundMatchResponse
{
    public Guid MatchId { get; init; }
    public int TimeToAccept { get; init; } = AshcrownApiConstants.TimeToAcceptMatchFoundSeconds;
    public required FindMatchType MatchType { get; init; }
    public required string OpponentName { get; init; }
    public required string OpponentIcon { get; init; }
    public required string OpponentCrown { get; init; }
    public string[]? OpponentChampions { get; init; }
}
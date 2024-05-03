namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestFindMatch : PlayerRequest
{
    public required MatchType MatchType { get; set; }
    public string? PrivateOpponentName { get; set; }
}
using Ashcrown.Remake.Api.Models.Enums;

namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestFindMatch : PlayerRequest
{
    public required FindMatchType MatchType { get; set; }
    public string? PrivateOpponentName { get; set; }
}
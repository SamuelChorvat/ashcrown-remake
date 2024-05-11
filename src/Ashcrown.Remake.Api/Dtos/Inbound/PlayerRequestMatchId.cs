namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestMatchId : PlayerRequest
{
    public required Guid MatchId { get; set; }
}
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestEndTurn : PlayerRequestMatchId
{
    public required EndTurn EndTurn { get; init; }
}
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestGetTargets : PlayerRequestMatchId
{
    public required GetTargets GetTargets { get; init; }
}
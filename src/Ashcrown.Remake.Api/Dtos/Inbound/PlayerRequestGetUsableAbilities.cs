using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Api.Dtos.Inbound;

public class PlayerRequestGetUsableAbilities : PlayerRequestMatchId
{
    public required GetUsableAbilities GetUsableAbilities { get; init; }
}
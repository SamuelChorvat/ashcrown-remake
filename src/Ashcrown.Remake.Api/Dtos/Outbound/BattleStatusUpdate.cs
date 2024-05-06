using Ashcrown.Remake.Core.Battle.Enums;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

namespace Ashcrown.Remake.Api.Dtos.Outbound;

public class BattleStatusUpdate
{
    public required BattleStatus BattleStatus { get; init; }
    public PlayerUpdate? PlayerUpdate { get; init; }
}
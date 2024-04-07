using Ashcrown.Remake.Core.Ability.Enums;

namespace Ashcrown.Remake.Core.Battle.Models.Dtos;

public class AbilityUpdate
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cooldown { get; set; }
    public required int ReadyIn { get; set; }
    public required int[] Cost { get; set; }
    public required bool CanUse { get; set; }
    public required AbilityTarget Target { get; set; }
    public required bool SelfDisplay { get; set; }
    public required bool SelfCast { get; set; }
    public required AbilityClass[] Classes { get; set; }
}
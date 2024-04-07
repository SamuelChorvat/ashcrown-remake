namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class ActiveEffectUpdate
{
    public required string OriginAbilityName { get; set; }
    public required string Description { get; set; }
    public required int Stacks { get; set; }
    public required bool MeCast { get; set; }
}
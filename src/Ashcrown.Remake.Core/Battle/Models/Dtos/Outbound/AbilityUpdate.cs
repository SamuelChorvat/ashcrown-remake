namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class AbilityUpdate
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required int Cooldown { get; set; }
    public required int ReadyIn { get; set; }
    public required int[] Cost { get; set; }
    public required bool CanUse { get; set; }
    public required string Target { get; set; }
    public required bool SelfDisplay { get; set; }
    public required bool SelfCast { get; set; }
    public required string[] Classes { get; set; }
}
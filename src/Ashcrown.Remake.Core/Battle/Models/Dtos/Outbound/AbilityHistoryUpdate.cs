namespace Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

public class AbilityHistoryUpdate
{
    public required int TurnNo { get; set; }
    public required string PlayerName { get; set; }
    public required string CasterName { get; set; }
    public required string AbilityName { get; set; }
    public required string AbilityDescription { get; set; }
    public required bool AbilityFree { get; set; }
    public required int[] AbilityCost { get; set; }
    public required string AbilityClasses { get; set; }
    public required int AbilityCooldown { get; set; }
    public required IList<string> TargetNames { get; set; }
}
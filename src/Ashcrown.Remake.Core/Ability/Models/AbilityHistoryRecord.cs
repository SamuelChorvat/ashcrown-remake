namespace Ashcrown.Remake.Core.Ability.Models;

public class AbilityHistoryRecord
{
    public required int TurnNo { get; set; }
    public required int ParticipantNo { get; set; }
    public required string ParticipantName { get; set; }
    public required string CasterName { get; set; }
    public required string AbilityName { get; set; }
    public required string AbilityDescription { get; set; }
    public required IEnumerable<int> AbilityCost { get; set; }
    public required IEnumerable<string> AbilityClasses { get; set; }
    public required int AbilityCooldown { get; set; }
}
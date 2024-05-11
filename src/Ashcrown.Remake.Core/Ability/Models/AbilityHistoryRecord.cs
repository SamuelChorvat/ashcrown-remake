using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Outbound;

namespace Ashcrown.Remake.Core.Ability.Models;

public class AbilityHistoryRecord
{
    public required int TurnNo { get; set; }
    public required int PlayerNo { get; set; }
    public required string PlayerName { get; set; }
    public required string CasterName { get; set; }
    public required string AbilityName { get; set; }
    public required string AbilityDescription { get; set; }
    public required int[] AbilityCost { get; set; }
    public required IList<AbilityClass> AbilityClasses { get; set; }
    public required int AbilityCooldown { get; set; }
    public List<string> TargetNames { get; } = [];
    public required bool Invisible { get; set; }

    private string GetClassesAsString() {
        var toReturn = "";

        for (var i = 0; i < AbilityClasses.Count; i++) {
            if (i != 0) {
                toReturn += ", ";
            }
            toReturn += AbilityClasses[i].ToString();
        }

        return toReturn;
    }

    public bool IsFree()
    {
        return AbilityCost.All(amount => amount <= 0);
    }

    public AbilityHistoryUpdate GetAbilityHistoryUpdate()
    {
        return new AbilityHistoryUpdate
        {
            TurnNo = TurnNo,
            PlayerName = PlayerName,
            CasterName = CasterName,
            AbilityName = AbilityName,
            AbilityDescription = AbilityDescription,
            AbilityFree = IsFree(),
            AbilityCost = AbilityCost,
            AbilityClasses = GetClassesAsString(),
            AbilityCooldown = AbilityCooldown,
            TargetNames = TargetNames
        };
    }
}
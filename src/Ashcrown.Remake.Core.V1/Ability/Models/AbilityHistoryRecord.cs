using Ashcrown.Remake.Core.V1.Ability.Enums;

namespace Ashcrown.Remake.Core.V1.Ability.Models;

public interface AbilityHistoryRecord
{
    public int TurnNo { get; set; }
    public int PlayerNo { get; set; }
    public string PlayerName { get; set; }
    public string CasterName { get; set; }
    public string AbilityName { get; set; }
    public string AbilityDescription { get; set; }
    public int[] AbilityCost { get; set; }
    public List<AbilityClass> AbilityClasses { get; set; }
    public int AbilityCooldown { get; set; }
    public List<string> TargetNames { get; }
    public bool Invisible { get; set; }

    public string GetClassesAsString() {
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
}
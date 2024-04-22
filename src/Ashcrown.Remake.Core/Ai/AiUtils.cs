using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Core.Ai;

public class AiUtils : IAiUtils
{
    public static AiMaximizedAbility GetHigherPointsAbility(AiMaximizedAbility? ability1, AiMaximizedAbility ability2)
    {
        if (ability1 == null) {
            return ability2;
        }

        if (ability2.Points > ability1.Points) {
            return ability2;
        }

        if (ability2.Points != ability1.Points) return ability1;
        var random = new Random();
        return random.Next(2) == 1 ? ability2 : ability1;
    }

    public static EndTurn PackSelectedAbilities(IList<AiMaximizedAbility> selectedAbilities)
    {
        var endTurn = new EndTurn
        {
            EndTurnAbilities = new List<EndTurnAbility>()
        };

        for (var i = 0; i < selectedAbilities.Count; i++) {
            endTurn.EndTurnAbilities.Add(new EndTurnAbility
            {
                CasterNo = selectedAbilities[i].CasterNo,
                AbilityNo = selectedAbilities[i].AbilityNo,
                Targets = selectedAbilities[i].Targets,
                Order = i + 1
            });
        }

        return endTurn;
    }
}
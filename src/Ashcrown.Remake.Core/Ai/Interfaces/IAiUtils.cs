using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Models.Dtos.Inbound;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiUtils
{
    static abstract AiMaximizedAbility GetHigherPointsAbility(AiMaximizedAbility? ability1, AiMaximizedAbility ability2);
    static abstract EndTurn PackSelectedAbilities(IList<AiMaximizedAbility> selectedAbilities);
}
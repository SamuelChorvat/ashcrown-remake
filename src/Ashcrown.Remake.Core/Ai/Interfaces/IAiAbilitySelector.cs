using Ashcrown.Remake.Core.Ai.Models;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiAbilitySelector
{
    IList<AiMaximizedAbility> SelectAbilities<TAiUtils, TAiPointsCalculator>() where TAiUtils : IAiUtils
        where TAiPointsCalculator : IAiPointsCalculator;
}
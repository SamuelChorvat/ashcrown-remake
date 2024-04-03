using Ashcrown.Remake.Core.V1.Ai.Models;

namespace Ashcrown.Remake.Core.V1.Ai.Interfaces;

public interface IAiEnergySelector
{
    int[] SelectEnergyToSpend(IList<AiMaximizedAbility> selectedAbilities);
}
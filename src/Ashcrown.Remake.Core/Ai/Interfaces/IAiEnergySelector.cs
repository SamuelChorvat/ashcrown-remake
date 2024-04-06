using Ashcrown.Remake.Core.Ai.Models;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiEnergySelector
{
    int[] SelectEnergyToSpend(IList<AiMaximizedAbility> selectedAbilities);
}
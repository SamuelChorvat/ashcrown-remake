using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ai.Interfaces;

public interface IAiEnergyUsageController
{
    IList<AiEnergyUsage> EnergyUsages { get; init; }
    void Reset();
    void IncrementEnergyUsage(EnergyType energyType, int incrementBy);
    void CalculateEnergyUsage(IChampion[] champions);
    EnergyType GetLeastUsedEnergyType(int[] energyLeftForSpending);
}
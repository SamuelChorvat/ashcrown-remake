using Ashcrown.Remake.Core.Ability.Enums;

namespace Ashcrown.Remake.Core.Ai.Models;

public class AiEnergyLeft(int[] currentEnergy)
{
    public int[] CurrentEnergy { get; init; } = currentEnergy;
    public int ToSubtract { get; set; } = 0;

    public void SubtractCurrentEnergy(EnergyType energyType, int amountToSubtract) {
        CurrentEnergy[(int) energyType] -= amountToSubtract;
    }
}
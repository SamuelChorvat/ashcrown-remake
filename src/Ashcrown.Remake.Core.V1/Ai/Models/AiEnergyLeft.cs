using Ashcrown.Remake.Core.Ability.Enums;

namespace Ashcrown.Remake.Core.V1.Ai.Models;

public class AiEnergyLeft
{
    public required int[] CurrentEnergy { get; init; }
    public int ToSubtract { get; set; } = 0;

    public void SubtractCurrentEnergy(EnergyType energyType, int amountToSubtract) {
        CurrentEnergy[(int) energyType] -= amountToSubtract;
    }
}
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Battle.Interfaces;

namespace Ashcrown.Remake.Core.Ai;

public class AiEnergySelector(
    IBattleLogic battleLogic,
    IAiEnergyUsageController aiEnergyUsageController) : IAiEnergySelector
{
    public int[] SelectEnergyToSpend(IList<AiMaximizedAbility> selectedAbilities)
    {
        aiEnergyUsageController.CalculateEnergyUsage(battleLogic.GetAiOpponentBattlePlayer().Champions);

        var energyToSpendWithoutRandom = GetEnergyToSpendWithoutRandom(selectedAbilities);
        var energyLeftToSpend = GetEnergyLeftForSpending(battleLogic.GetAiOpponentBattlePlayer(), 
            energyToSpendWithoutRandom);

        var randomEnergyToSpend = GetTotalRandomEnergyToSpend(selectedAbilities);

        return GetEnergyToSpend(energyToSpendWithoutRandom, energyLeftToSpend, randomEnergyToSpend);
    }

    private static int[] GetEnergyToSpendWithoutRandom(IEnumerable<AiMaximizedAbility> selectedAbilities)
    {
        var energyToSpend = new int[4];

        foreach (var aiMaximizedAbility in selectedAbilities) {
            var currentCost = aiMaximizedAbility.Ability!.GetCurrentCost();
            for (var i = 0; i <= 3; i++) {
                energyToSpend[i] += currentCost[i];
            }
        }

        return energyToSpend;
    }
    
    private static int[] GetEnergyLeftForSpending(IBattlePlayer aiBattlePlayer, IReadOnlyList<int> energyToSpendWithoutRandom)
    {
        var energyLeftForSpending = aiBattlePlayer.Energy.ToArray();

        for (var i = 0; i < energyLeftForSpending.Length; i++) {
            energyLeftForSpending[i] -= energyToSpendWithoutRandom[i];
        }

        return energyLeftForSpending;
    }
    
    private static int GetTotalRandomEnergyToSpend(IList<AiMaximizedAbility> selectedAbilities)
    {
        return selectedAbilities.Sum(aiMaximizedAbility => aiMaximizedAbility.Ability!.GetCurrentCost()[4]);
    }
    
    private int[] GetEnergyToSpend(int[] energyToSpendWithoutRandom, int[] energyLeftToSpend, int randomEnergyToSpend)
    {
        if (energyLeftToSpend.Sum() == randomEnergyToSpend) {
            return battleLogic.GetAiOpponentBattlePlayer().Energy.ToArray();
        }

        var randomEnergyLeftToSpend = randomEnergyToSpend;

        while (randomEnergyLeftToSpend > 0) {
            int index;
            try {
                index = (int) aiEnergyUsageController.GetLeastUsedEnergyType(energyLeftToSpend);
            } catch (Exception) {
                battleLogic.EndBattleOnAiError("Exception getting least used energy");
                return [];
            }
            energyToSpendWithoutRandom[index] += 1;
            energyLeftToSpend[index] -= 1;
            randomEnergyLeftToSpend -= 1;
        }

        return energyToSpendWithoutRandom;
    }
}
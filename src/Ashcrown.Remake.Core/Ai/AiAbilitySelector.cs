using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ai.Interfaces;
using Ashcrown.Remake.Core.Ai.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Ai;

public class AiAbilitySelector(IAiController aiController) : IAiAbilitySelector
{
    public IList<AiMaximizedAbility> SelectAbilities()
    {
        ResetAiActive();
        ResetAiAbilitySelected();
        ResetAiLethal();
        ResetAiTotalDamageToReceiveAfterDestructible();
        ResetAiTotalHealingToReceive();
        ResetAiTotalDestructibleDefenseLeft();
        
        var toReturn = new List<AiMaximizedAbility>();
        for (var i = 0; i < aiController.BattleLogic.GetAiOpponentBattlePlayer().Champions.Length; i++) {
            var maximizedAbility = GetMaximizedAbility();
            if (maximizedAbility is {Points: > 0}) {
                toReturn.Add(maximizedAbility);
                var aiEnergyLeft = GetEnergyLeft(toReturn);
                UpdateAiActive(aiEnergyLeft.CurrentEnergy, aiEnergyLeft.ToSubtract);
            } else {
                break;
            }
        }

        return toReturn;
    }

    private void ResetAiActive()
    {
        UpdateAiActive(aiController.BattleLogic.GetAiOpponentBattlePlayer().Energy, 0);
    }

    private void UpdateAiActive(int[] energy, int toSubtract)
    {
        foreach (var champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().Champions)
        {
            champion.AbilityController.SetAiActiveAbilities(energy, toSubtract);
        }
    }

    private void ResetAiAbilitySelected()
    {
        foreach (var champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().Champions) {
            champion.AbilityController.AiAbilitySelected = false;
        }
    }

    private void ResetAiLethal()
    {
        foreach (var champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().GetEnemyPlayer().Champions) {
            champion.AiLethal = false;
        }
    }

    private void ResetAiTotalDamageToReceiveAfterDestructible()
    {
        foreach (var champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().GetEnemyPlayer().Champions) {
            champion.AiTotalDamageToReceiveAfterDestructible = 0;
        }
    }

    private void ResetAiTotalHealingToReceive()
    {
        foreach (var champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().Champions) {
            champion.AiTotalHealingToReceive = 0;
        }
    }

    private void ResetAiTotalDestructibleDefenseLeft()
    {
        foreach (var champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().GetEnemyPlayer().Champions) {
            champion.AiTotalDestructibleDefenseLeft = GetTotalDestructibleDefense(champion);
        }
    }

    private static int GetTotalDestructibleDefense(IChampion champion)
    {
        return champion.ActiveEffects.Where(activeEffect => activeEffect.DestructibleDefense > 0)
            .Sum(activeEffect => activeEffect.DestructibleDefense);
    }
    
    private AiMaximizedAbility? GetMaximizedAbility()
    {
        var maximizedAbilities = 
            (from champion in aiController.BattleLogic.GetAiOpponentBattlePlayer().Champions 
                where !champion.AbilityController.AiAbilitySelected 
                select champion.AbilityController.GetBestMaximizedAbility<AiUtils>())
            .ToList();

        if (IsAllNull(maximizedAbilities)) {
            return null;
        }

        var toReturn = PickMaximizedAbilityHelper<AiUtils>(maximizedAbilities);
        toReturn.Champion!.AbilityController.AiAbilitySelected = true;

        return toReturn;
    }
    
    private static bool IsAllNull(IEnumerable<AiMaximizedAbility?> maximizedAbilities)
    {
        return !maximizedAbilities.OfType<AiMaximizedAbility>().Any();
    }

    private static AiMaximizedAbility PickMaximizedAbilityHelper<T>(List<AiMaximizedAbility?> maximizedAbilities) where T : IAiUtils
    {
        AiMaximizedAbility toReturn = null!;
        foreach (var aiMaximizedAbility in maximizedAbilities) {
            if (aiMaximizedAbility == null) {
                continue;
            }

            toReturn = T.GetHigherPointsAbility(toReturn, aiMaximizedAbility);
        }

        return toReturn;
    }

    private AiEnergyLeft GetEnergyLeft(IEnumerable<AiMaximizedAbility?> aiMaximizedAbilities)
    {
        var energyLeft = new AiEnergyLeft(aiController.BattleLogic.GetAiOpponentBattlePlayer().Energy);

        foreach (var aiMaximizedAbility in aiMaximizedAbilities) {
            if (aiMaximizedAbility == null) {
                continue;
            }

            for (var j = 0; j <= 3; j++) {
                energyLeft.SubtractCurrentEnergy((EnergyType) j, aiMaximizedAbility.Ability!.GetCurrentCost()[j]);
            }

            energyLeft.ToSubtract += aiMaximizedAbility.Ability!.GetCurrentCost()[4];
        }

        return energyLeft;
    }
}
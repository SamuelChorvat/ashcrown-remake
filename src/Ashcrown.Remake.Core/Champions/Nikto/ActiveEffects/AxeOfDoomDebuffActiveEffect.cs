using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Nikto.ActiveEffects;

public class AxeOfDoomDebuffActiveEffect
{
    //TODO Refactor this?
    public static void ReduceStunDuration(IChampion dealer, IActiveEffect activeEffect)
    {
        
    }
    
    //TODO Refactor this?
    public static void ReduceDealActiveEffectEnergyStealRemoveAmount(IChampion dealer, IActiveEffect activeEffect)
    {
        
    }

    //TODO Refactor this?
    public static int ReduceDealAbilityEnergyStealRemoveAmount(IChampion dealer, IAbility ability, int energyAmount)
    {
        return energyAmount;
    }
}
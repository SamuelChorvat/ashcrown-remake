using Ashcrown.Remake.Core.V1.Ability.Interfaces;
using Ashcrown.Remake.Core.V1.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.V1.Champion.Interfaces;

namespace Ashcrown.Remake.Core.V1.Champions.Luther.ActiveEffects;

public class ForgeSpiritActiveEffect
{
    //TODO Refactor this?
    public static int AbilityDamageModifier(IChampion damageDealer, int amount, IAbility usedAbility)
    {
        return amount;
    }

    //TODO Refactor this?
    public static int ActiveEffectDamageModifier(IChampion damageDealer, int amount,
        IActiveEffect activeEffect)
    {
        return amount;
    }
}
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

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
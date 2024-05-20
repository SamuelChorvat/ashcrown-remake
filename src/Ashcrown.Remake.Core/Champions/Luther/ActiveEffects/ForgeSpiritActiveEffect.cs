using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class ForgeSpiritActiveEffect : ActiveEffectBase
{
    public ForgeSpiritActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.ForgeSpiritActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        
        Description = "- All original non-affliction damage this champion deals will be reduced to a maximum of 30";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    //TODO Refactor this
    public static int AbilityDamageModifier(IChampion damageDealer, int amount, IAbility usedAbility)
    {
        if (damageDealer.ChampionController.IsIgnoringHarmful()) {
            return amount;
        }

        if(damageDealer.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LutherConstants.ForgeSpiritActiveEffect) 
           && amount > 30 && !usedAbility.AfflictionDamage) {
            return 30;
        }

        return amount;
    }

    //TODO Refactor this
    public static int ActiveEffectDamageModifier(IChampion damageDealer, int amount,
        IActiveEffect activeEffect)
    {
        if(damageDealer.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LutherConstants.ForgeSpiritActiveEffect) 
           && amount > 30 && !activeEffect.AfflictionDamage) {
            return 30;
        }

        return amount;
    }
}
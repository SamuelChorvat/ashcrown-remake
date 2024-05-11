using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

// ReSharper disable twice InconsistentNaming
public class EMPBurstActiveEffect : ActiveEffectBase
{
    public EMPBurstActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CronosConstants.EMPBurstActiveEffect, originAbility, championTarget)
    {
        Description = "- This champion will take 5 additional damage from Affliction abilities";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Infinite = true;
    }

    //TODO Refactor this
    public static int ApplyEMPBurstAfflictionDamageIncrease(IChampion victim, int damage, IAbility? ability = null,
        IActiveEffect? activeEffect = null)
    {
        if (ability is { AfflictionDamage: true }) {
            return damage + 5 * victim.ActiveEffectController.GetActiveEffectCountByName(CronosConstants.EMPBurstActiveEffect);
        }

        if (activeEffect is { AfflictionDamage: true }) {
            return damage + 5 * victim.ActiveEffectController.GetActiveEffectCountByName(CronosConstants.EMPBurstActiveEffect);
        }

        return damage;
    }
}
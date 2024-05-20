using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class LivingForgeBuffActiveEffect : ActiveEffectBase
{
    public LivingForgeBuffActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.LivingForgeBuffActiveEffect, originAbility, championTarget)
    {
        Duration1 = 2;
        DealDamageIncreasePoint1 = originAbility.DealDamageIncreasePoint1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Buff = true;
        Helpful = true;
        AllDamageDealModifier = new PointsPercentageModifier(DealDamageIncreasePoint1);
        Stackable = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return $"- This champion will deal an additional {DealDamageIncreasePoint1*Stacks} damage with their abilities"
               + GetTimeLeftAffix();
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        Stacks += 1;
        AllDamageDealModifier = new PointsPercentageModifier(DealDamageIncreasePoint1 * Stacks);
    }
}
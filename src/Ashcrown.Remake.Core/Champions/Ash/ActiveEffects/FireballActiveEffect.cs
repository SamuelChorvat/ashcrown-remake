using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.ActiveEffects;

public class FireballActiveEffect : ActiveEffectBase
{
    public FireballActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AshConstants.FireballActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        DealDamageReductionPoint1 = originAbility.DealDamageReductionPoint1;
        
        Description = $"- This champion's non-Affliction abilities will deal {DealDamageReductionPoint1} less damage";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageDealModifier = new PointsPercentageModifier(-DealDamageReductionPoint1);
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
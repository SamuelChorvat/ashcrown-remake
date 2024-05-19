using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.ActiveEffects;

public class MortalStrikeActiveEffect : ActiveEffectBase
{
    public MortalStrikeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(KhanConstants.MortalStrikeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        DealHealReductionPercent1 = originAbility.DealHealReductionPercent1 ;
        ReceiveHealReductionPercent1 = originAbility.ReceiveHealReductionPercent1;
        
        Description = $"- This champion will receive {ReceiveHealReductionPercent1}% less healing\n" +
                           $"- This champion will deal {DealHealReductionPercent1}% less healing";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        HealingDealModifier = new PointsPercentageModifier(0, -DealHealReductionPercent1);
        HealingReceiveModifier = new PointsPercentageModifier(0, -ReceiveHealReductionPercent1);
    }
}
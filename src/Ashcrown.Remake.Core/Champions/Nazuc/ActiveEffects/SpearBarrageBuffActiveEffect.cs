using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.ActiveEffects;

public class SpearBarrageBuffActiveEffect : ActiveEffectBase
{
    public SpearBarrageBuffActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(NazucConstants.SpearBarrageBuffActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n" +
                           $"- {NazucConstants.SpearThrow.HighlightInGold()} is improved and its cost is reduced by <sprite=4>";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1, 0);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(NazucConstants.SpearThrow) != null) {
            Target.AbilityController.GetMyAbilityByName(NazucConstants.SpearThrow)!.RandomCostModifier = -1;
        }
    }

    public override void OnRemove()
    {
        if (Target.AbilityController.GetMyAbilityByName(NazucConstants.SpearThrow) != null) {
            Target.AbilityController.GetMyAbilityByName(NazucConstants.SpearThrow)!.RemoveCostModifier(1);
        }
    }
}
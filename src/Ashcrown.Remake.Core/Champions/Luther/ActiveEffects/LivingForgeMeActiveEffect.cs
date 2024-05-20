using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class LivingForgeMeActiveEffect : ActiveEffectBase
{
    public LivingForgeMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LutherConstants.LivingForgeMeActiveEffect, originAbility, championTarget)
    {
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        Duration1 = originAbility.Duration1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
                           + $"- {LutherConstants.ForgeSpirit.HighlightInGold()} can be used";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(LutherConstants.ForgeSpirit) != null) {
            Target.CurrentAbilities[2] = Target.AbilityController.GetMyAbilityByName(LutherConstants.ForgeSpirit)!;
        }
    }

    public override void OnRemove()
    {
        if (Target.ActiveEffectController
                .GetActiveEffectCountByName(LutherConstants.LivingForgeMeActiveEffect) != 1) return;
        if (Target.AbilityController.GetMyAbilityByName(LutherConstants.LivingForge) != null) {
            Target.CurrentAbilities[2] = Target.AbilityController.GetMyAbilityByName(LutherConstants.LivingForge)!;
        }
    }
}
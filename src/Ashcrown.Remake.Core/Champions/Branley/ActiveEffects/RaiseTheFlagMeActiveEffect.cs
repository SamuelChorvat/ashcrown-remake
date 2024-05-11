using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.ActiveEffects;

public class RaiseTheFlagMeActiveEffect : ActiveEffectBase
{
    public RaiseTheFlagMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(BranleyConstants.RaiseTheFlagMeActiveEffect, originAbility, championTarget)
    {
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        Duration1 = originAbility.Duration1 + 1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
                           + $"- {BranleyConstants.Plunder.HighlightInGold()} {"ignores invulnerability".HighlightInPurple()} and deals an additional {$"15 {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void OnAdd()
    {
        if (Target.AbilityController.GetMyAbilityByName(BranleyConstants.Plunder) != null) {
            Target.AbilityController.GetMyAbilityByName(BranleyConstants.Plunder)!.IgnoreInvulnerability = true;
        }
    }

    public override void OnRemove()
    {
        if (Target.ActiveEffectController.GetActiveEffectCountByName(BranleyConstants.RaiseTheFlagMeActiveEffect) !=
            1) return;
        if (Target.AbilityController.GetMyAbilityByName(BranleyConstants.Plunder) != null) {
            Target.AbilityController.GetMyAbilityByName(BranleyConstants.Plunder)!.IgnoreInvulnerability = false;
        }
    }
}
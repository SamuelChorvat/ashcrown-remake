using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.ActiveEffects;

public class HuntersFocusStacksActiveEffect : ActiveEffectBase
{
    public HuntersFocusStacksActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(BrayaConstants.HuntersFocusStacksActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Stacks = 4;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        var stacks = Stacks > 1 ? "stacks" : "stack";
        return $"- {Target.Name} has {Stacks} {BrayaConstants.HuntersFocus.HighlightInGold()} {stacks}"
               + GetTimeLeftAffix();
    }

    public override void OnApply()
    {
        if (Stacks <= 0) {
            RemoveIt = true;
        }
    }

    public override bool CustomDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (!Target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(BrayaConstants
                .HuntersFocusStacksActiveEffect)) return false;
        dealer.ChampionController.DealActiveEffect(Target, OriginAbility,
            new HuntersFocusIgnoreActiveEffect(OriginAbility, Target), false, appliedAdditionalLogic);
        return true;
    }

    public override void EndTurnChecks()
    {
        if (Stacks <= 0) {
            Target.ActiveEffectController.RemoveActiveEffect(this);
        }
    }
}
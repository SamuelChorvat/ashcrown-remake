using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;

namespace Ashcrown.Remake.Core.Champions.Cedric.ActiveEffects;

public class TimeWarpActiveEffect : ActiveEffectBase
{
    public TimeWarpActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CedricConstants.TimeWarpActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = "- This champion's ability costs are reduced by <sprite=4>\n"
                           + "- This champion's ability cooldowns are reduced by 1 turn";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void OnAdd()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < Target.Abilities[i].Count; j++) {
                Target.Abilities[i][j].CooldownModifier = -1;
                Target.Abilities[i][j].RandomCostModifier = -1;
            }
        }
    }

    public override void OnRemove()
    {
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < Target.Abilities[i].Count; j++) {
                Target.Abilities[i][j].RemoveCooldownModifier(1);
                Target.Abilities[i][j].RemoveCostModifier(1);
            }
        }
    }

    public override void AdditionalDealActiveEffectLogic(IChampion dealer, IChampion target, IAbility ability, bool secondary,
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        if (dealer.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(CedricConstants.MirrorImageActiveEffect)) {
            TimeLeft += dealer.ActiveEffectController
                .GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect)!.Stacks;
        }
    }
}
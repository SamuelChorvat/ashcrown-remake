using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;

namespace Ashcrown.Remake.Core.Champions.Hrom.ActiveEffects;

public class LightningBarrierActiveEffect : ActiveEffectBase
{
    public LightningBarrierActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HromConstants.LightningBarrierActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- First harmful non-Strategic ability used on this champion will be {"countered".HighlightInPurple()}\n"
                           + $"- This ability is {"invisible".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Hidden = true;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void OnRemove()
    {
        if(!Target.ActiveEffectController.ActiveEffectPresentByActiveEffectName(HromConstants.LightningBarrierEndActiveEffect)) {
            OriginAbility.Owner.ChampionController.DealActiveEffect(Target, OriginAbility, 
                new LightningBarrierEndActiveEffect(OriginAbility, Target), true, new AppliedAdditionalLogic());
        }
    }

    public override bool CounterOnEnemy(IAbility ability)
    {
        if (ability.AbilityClassesContains(AbilityClass.Strategic) ||
            !ability.Harmful ||
            ability.Owner.BattlePlayer.PlayerNo == Target.BattlePlayer.PlayerNo) return false;
        OriginAbility.Owner.ChampionController.DealActiveEffect(ability.Owner, OriginAbility,
            new LightningBarrierCounterActiveEffect(OriginAbility, ability.Owner, ability.Name), 
            true, new AppliedAdditionalLogic());
        Target.ActiveEffectController.RemoveActiveEffect(this);

        return true;
    }
}
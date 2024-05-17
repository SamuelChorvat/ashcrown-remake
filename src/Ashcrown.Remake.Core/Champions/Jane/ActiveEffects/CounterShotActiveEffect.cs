using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.ActiveEffects;

public class CounterShotActiveEffect : ActiveEffectBase
{
    public CounterShotActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(JaneConstants.CounterShotActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        
        Description = $"- All new {"stun".HighlightInPurple()} abilities used by this champion will be decreased by 2 turns";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }

    //TODO Refactor this?
    public static void ReduceStunDuration(IChampion dealer, IActiveEffect activeEffect)
    {
        if (dealer.ChampionController.IsIgnoringHarmful()) {
            return;
        }

        if (activeEffect.Stun) {
            activeEffect.TimeLeft -= 2 * dealer.ActiveEffectController
                .GetActiveEffectCountByName(JaneConstants.CounterShotActiveEffect);
        }
    }
}
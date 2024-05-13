using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.ActiveEffects;

public class HuntersFocusIgnoreActiveEffect : ActiveEffectBase
{
    public HuntersFocusIgnoreActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(BrayaConstants.HuntersFocusIgnoreActiveEffect, originAbility, championTarget)
    {
        Duration2 = originAbility.Duration2;
        
        Description = $"- This champion will {"ignore".HighlightInPurple()} all harmful effects except energy cost changes";
        Duration = Duration2;
        TimeLeft = Duration2;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        IgnoreHarmful = originAbility.IgnoreHarmful;
        CannotBeRemoved = true;
    }
}
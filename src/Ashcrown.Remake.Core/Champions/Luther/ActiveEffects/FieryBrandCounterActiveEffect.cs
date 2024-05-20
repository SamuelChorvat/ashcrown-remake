using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Luther.Champion;

namespace Ashcrown.Remake.Core.Champions.Luther.ActiveEffects;

public class FieryBrandCounterActiveEffect : ActiveEffectBase
{
    public FieryBrandCounterActiveEffect(IAbility originAbility, IChampion championTarget, string counterName) 
        : base(LutherConstants.FieryBrandCounterActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- {counterName.HighlightInGold()} has been {"countered".HighlightInPurple()}\n"
                           + $"- {LutherConstants.Flamestrike.HighlightInGold()} will last 2 additional turns on this champion";
        Duration = Duration1;
        TimeLeft = Duration1;
    }
}
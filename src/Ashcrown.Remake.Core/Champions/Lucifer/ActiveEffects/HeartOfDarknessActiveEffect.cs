using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.ActiveEffect.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;

namespace Ashcrown.Remake.Core.Champions.Lucifer.ActiveEffects;

public class HeartOfDarknessActiveEffect : ActiveEffectBase
{
    public HeartOfDarknessActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(LuciferConstants.HeartOfDarknessActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = "- Rank 2\n"
                           + $"- This ability is {"invisible".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Hidden = true;
        Infinite = true;
        Stackable = true;
    }

    public override void AddStack(IActiveEffect activeEffect)
    {
        if (Description.Contains("Rank 1")) {
            Description = "- Rank 2\n" + "- This ability is <color=#FF00CD>invisible</color>";
        } else if (Description.Contains("Rank 2")) {
            Description = "- Rank 3\n" + "- This ability is <color=#FF00CD>invisible</color>";
        } else if (Description.Contains("Rank 3")) {
            Description = "- Rank 1\n" + "- This ability is <color=#FF00CD>invisible</color>";
        }
    }
}
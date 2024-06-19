using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nerissa.Champion;

namespace Ashcrown.Remake.Core.Champions.Nerissa.ActiveEffects;

public class DrownActiveEffect : ActiveEffectBase
{
    public DrownActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(NerissaConstants.DrownActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will receive {$"{Damage1} affliction damage".HighlightInRed()}\n" +
                           $"- {NerissaConstants.Overflow.HighlightInGold()} will deal an additional {$"5 {"piercing".HighlightInBold()} magic damage".HighlightInBlue()} to this enemy";
        Duration = Duration1;
        TimeLeft = Duration1;
        Infinite = true;
        Harmful = true;
        Damaging = originAbility.Damaging;
        AfflictionDamage = originAbility.AfflictionDamage;
        Debuff = originAbility.Debuff;
        Unique = true;
    }

    public override void OnApply()
    {
        OnApplyDot();
    }
}
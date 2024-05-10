using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Fae.Champion;

namespace Ashcrown.Remake.Core.Champions.Fae.ActiveEffects;

public class CorruptionActiveEffect : ActiveEffectBase
{
    public CorruptionActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(FaeConstants.CorruptionActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will receive {$"{Damage1} affliction damage".HighlightInRed()}";
        Duration = Duration1;
        TimeLeft = Duration1;
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
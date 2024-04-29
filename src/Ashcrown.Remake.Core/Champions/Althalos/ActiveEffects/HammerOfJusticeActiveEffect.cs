using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.ActiveEffects;

public class HammerOfJusticeActiveEffect : ActiveEffectBase
{
    public HammerOfJusticeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AlthalosConstants.HammerOfJusticeActiveEffect, 
            originAbility, 
            championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        Description = $"- This champion's Physical and Strategic abilities are {"stunned".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
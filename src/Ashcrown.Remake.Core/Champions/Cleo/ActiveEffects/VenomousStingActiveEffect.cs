using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;

namespace Ashcrown.Remake.Core.Champions.Cleo.ActiveEffects;

public class VenomousStingActiveEffect : ActiveEffectBase
{
    public VenomousStingActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CleoConstants.VenomousStingActiveEffect, originAbility, championTarget)
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
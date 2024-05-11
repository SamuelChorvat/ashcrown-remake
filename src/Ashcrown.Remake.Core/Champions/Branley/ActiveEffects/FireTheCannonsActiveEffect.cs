using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Branley.Champion;

namespace Ashcrown.Remake.Core.Champions.Branley.ActiveEffects;

public class FireTheCannonsActiveEffect : ActiveEffectBase
{
    public FireTheCannonsActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(BranleyConstants.FireTheCannonsActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        
        Description = $"- This champion's Physical and Affliction abilities are {"stunned".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
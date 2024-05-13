using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.ActiveEffects;

public class LoveChainsActiveEffect : ActiveEffectBase
{
    public LoveChainsActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GwenConstants.LoveChainsActiveEffect, originAbility, championTarget)
    {
		
        Duration1 = originAbility.Duration1 + 1;

        Description = $"- This champion is {"stunned".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
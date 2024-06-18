using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;

namespace Ashcrown.Remake.Core.Champions.Moroz.ActiveEffects;

public class FreezeTargetActiveEffect : ActiveEffectBase
{
    public FreezeTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(MorozConstants.FreezeTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Duration2 = originAbility.Duration2;
        
        Description = $"- This champion's non-Strategic abilities are {"stunned".HighlightInPurple()}\n"
                           + $"- This champion cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}\n"
                           + $"- {MorozConstants.Shatter.HighlightInGold()} can be used on this champion";
        Duration = Duration1 + 1;
        TimeLeft = Duration1 + 1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        DisableDamageReceiveReduction = originAbility.DisableDamageReceiveReduction;
        DisableInvulnerability = originAbility.DisableInvulnerability;
		
        EndsOnCasterDeath = true;
        EndsOnCasterStun = true;
        EndsOnTargetInvulnerability = true;
        EndsOnTargetDeath = true;
    }

    public override void OnApply()
    {
        TickDown();
		
        if (CasterLink!.TimeLeft != TimeLeft ) {
            CasterLink.TimeLeft = TimeLeft;
        }
    }
}
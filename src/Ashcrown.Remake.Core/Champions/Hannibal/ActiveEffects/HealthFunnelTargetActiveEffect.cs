using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Battle.Models;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.ActiveEffects;

public class HealthFunnelTargetActiveEffect : ActiveEffectBase
{
    public HealthFunnelTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HannibalConstants.HealthFunnelTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        Damage1 = originAbility.Damage1;
        
        Description = $"- This champion will have {$"{Damage1} health".HighlightInGreen()} {"stolen".HighlightInPurple()}\n"
                           + $"- This champion's non-Strategic abilities are {"stunned".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        Damaging = originAbility.Damaging;
        AfflictionDamage = originAbility.AfflictionDamage;
		
        EndsOnCasterDeath = true;
        EndsOnCasterStun = true;
        EndsOnTargetInvulnerability = true;
        EndsOnTargetDeath = true;
    }

    public override void OnApply()
    {
        OnApplyActionControlTargetDamage();
    }

    public override void AdditionalSubtractHealthLogic(int toSubtract, IChampion victim, 
        AppliedAdditionalLogic appliedAdditionalLogic)
    {
        OriginAbility.Owner.ChampionController.DealActiveEffectHealing(Math.Min(victim.Health,
            toSubtract), OriginAbility.Owner, this, appliedAdditionalLogic);
    }
}
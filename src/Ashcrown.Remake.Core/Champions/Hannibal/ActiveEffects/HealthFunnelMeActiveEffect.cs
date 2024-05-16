using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.ActiveEffects;

public class HealthFunnelMeActiveEffect : ActiveEffectBase
{
    public HealthFunnelMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HannibalConstants.HealthFunnelMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Source = true;
		
        EndsOnCasterDeath = true;
        EndsOnCasterStun = true;
        EndsOnTargetInvulnerability = true;
        EndsOnTargetDeath = true;
    }

    public override string GetDescriptionWithTimeLeftAffix(int playerNo)
    {
        return GetActionControlDescription();
    }

    public override void OnApply()
    {
        OnApplyActionControlMe();
    }
}
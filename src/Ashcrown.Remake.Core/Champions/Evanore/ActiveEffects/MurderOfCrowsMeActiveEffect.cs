using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;

namespace Ashcrown.Remake.Core.Champions.Evanore.ActiveEffects;

public class MurderOfCrowsMeActiveEffect : ActiveEffectBase
{
    public MurderOfCrowsMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(EvanoreConstants.MurderOfCrowsMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Source = true;
		
        EndsOnTargetDeath = true;
        PauseOnCasterStun = true;
        EndsOnCasterDeath = true;
        PauseOnTargetInvulnerability = true;
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
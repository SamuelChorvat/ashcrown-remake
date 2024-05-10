using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Dura.Champion;

namespace Ashcrown.Remake.Core.Champions.Dura.ActiveEffects;

public class SonicWavesMeActiveEffect : ActiveEffectBase
{
    public SonicWavesMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(DuraConstants.SonicWavesMeActiveEffect, originAbility, championTarget)
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
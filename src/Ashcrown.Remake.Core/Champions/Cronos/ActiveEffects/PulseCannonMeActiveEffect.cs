using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;

namespace Ashcrown.Remake.Core.Champions.Cronos.ActiveEffects;

public class PulseCannonMeActiveEffect : ActiveEffectBase
{
    public PulseCannonMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(CronosConstants.PulseCannonMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Duration = Duration1;
        TimeLeft = Duration1;
        Source = true;

        EndsOnTargetDeath = true;
        EndsOnCasterDeath = true;
        PauseOnCasterStun = true;
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
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Khan.Champion;

namespace Ashcrown.Remake.Core.Champions.Khan.ActiveEffects;

public class BladestormMeActiveEffect : ActiveEffectBase
{
    public BladestormMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(KhanConstants.BladestormMeActiveEffect, originAbility, championTarget)
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
        return $"- {KhanConstants.MortalStrike.HighlightInGold()} will deal an additional {"10 physical damage".HighlightInOrange()}\n" +
               GetActionControlDescription();
    }

    public override void OnApply()
    {
        OnApplyActionControlMe();
    }
}
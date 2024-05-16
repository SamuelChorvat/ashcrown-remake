using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;

namespace Ashcrown.Remake.Core.Champions.Hannibal.ActiveEffects;

public class SacrificialPactActiveEffect : ActiveEffectBase
{
    public SacrificialPactActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(HannibalConstants.SacrificialPactActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- This ability is {"invisible".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Hidden = true;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Infinite = true;
    }

    public override void AfterSubtractHealthLogic()
    {
        if (Target.Health <= 25 && OriginAbility.Owner.Died) {
            Target.ChampionController.OnDeath();
        }
    }

    public override void AdditionalProcessDeathLogic()
    {
        if (!OriginAbility.Owner.Alive) {
            Target.SacrificialPactTriggered = true;
        }
    }
}
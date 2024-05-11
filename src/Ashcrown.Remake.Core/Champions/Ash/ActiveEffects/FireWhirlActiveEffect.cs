using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Ash.Champion;

namespace Ashcrown.Remake.Core.Champions.Ash.ActiveEffects;

public class FireWhirlActiveEffect : ActiveEffectBase
{
    public FireWhirlActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AshConstants.FireWhirlActiveEffect, originAbility, championTarget)
    {
        Damage1 = originAbility.Damage1;
        Duration1 = originAbility.Duration1;
        
        Description = $"- This champion is {"stunned".HighlightInPurple()}";
        Duration = Duration1 + 1;
        TimeLeft = Duration1 + 1;
        Stun = originAbility.Stun;
        StunType = originAbility.StunType;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;

namespace Ashcrown.Remake.Core.Champions.Eluard.ActiveEffects;

public class DevastateActiveEffect : ActiveEffect.Abstract.ActiveEffect
{
    public DevastateActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(EluardConstants.DevastateActiveEffect, 
            originAbility, championTarget)
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
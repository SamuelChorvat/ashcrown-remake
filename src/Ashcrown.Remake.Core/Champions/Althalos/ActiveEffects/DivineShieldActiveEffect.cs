using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.ActiveEffects;

public class DivineShieldActiveEffect : ActiveEffect.Abstract.ActiveEffect
{
    public DivineShieldActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AlthalosConstants.DivineShieldActiveEffect, 
            originAbility, 
            championTarget)
    {
        Duration1 = originAbility.Duration1;
        Description = $"- This champion is {"invulnerable".HighlightInPurple()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Invulnerability = originAbility.Invulnerability;
        TypeOfInvulnerability = originAbility.TypeOfInvulnerability;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
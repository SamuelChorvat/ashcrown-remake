using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Braya.Champion;

namespace Ashcrown.Remake.Core.Champions.Braya.ActiveEffects;

public class QuickShotActiveEffect : ActiveEffectBase
{
    public QuickShotActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(BrayaConstants.QuickShotActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- This champion is {"invulnerable".HighlightInPurple()} to to all non-Strategic abilities";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        Invulnerability = originAbility.Invulnerability;
        TypeOfInvulnerability = originAbility.TypeOfInvulnerability;
    }
}
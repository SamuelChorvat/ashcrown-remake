using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Jane.Champion;

namespace Ashcrown.Remake.Core.Champions.Jane.ActiveEffects;

public class FlashbangActiveEffect : ActiveEffectBase
{
    public FlashbangActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(JaneConstants.FlashbangActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        
        Description = $"- This champion is {"invulnerable".HighlightInPurple()} to all non-Affliction abilities";
        Duration = Duration1;
        TimeLeft = Duration1;
        Invulnerability = originAbility.Invulnerability;
        TypeOfInvulnerability = originAbility.TypeOfInvulnerability;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;

namespace Ashcrown.Remake.Core.Champions.Nazuc.ActiveEffects;

public class HuntersMarkActiveEffect : ActiveEffectBase
{
    public HuntersMarkActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(NazucConstants.HuntersMarkActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1 + 1;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- This champion cannot {"reduce damage".HighlightInPurple()} or become {"invulnerable".HighlightInPurple()}"
                           + $"\n- {NazucConstants.SpearBarrage.HighlightInGold()} and {NazucConstants.SpearThrow.HighlightInGold()} will deal an additional {$"{BonusDamage1} physical damage".HighlightInOrange()} to this champion";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
        DisableDamageReceiveReduction = originAbility.DisableDamageReceiveReduction;
        DisableInvulnerability = originAbility.DisableInvulnerability;
        Unique = true;
    }
}
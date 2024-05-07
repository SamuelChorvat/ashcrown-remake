using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class ReapTriggeredTargetActiveEffect : ActiveEffectBase
{
    public ReapTriggeredTargetActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AzraelConstants.ReapTriggeredTargetActiveEffect, originAbility, championTarget)
    {
        Duration1 = 1;
        BonusDamage1 = originAbility.BonusDamage1;
        
        Description = $"- {AzraelConstants.Reap.HighlightInGold()} will deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()} to this champion";
        Duration = Duration1;
        TimeLeft = Duration1;
        Harmful = originAbility.Harmful;
        Debuff = originAbility.Debuff;
    }
}
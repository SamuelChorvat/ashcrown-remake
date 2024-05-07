using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;

namespace Ashcrown.Remake.Core.Champions.Azrael.ActiveEffects;

public class ReapMeActiveEffect : ActiveEffectBase
{
    public ReapMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AzraelConstants.ReapMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = 2;
        BonusDamage1 = originAbility.BonusDamage2;
        
        Description = $"- {AzraelConstants.Reap.HighlightInGold()} will deal an additional {$"{BonusDamage1} {"piercing".HighlightInBold()} physical damage".HighlightInOrange()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }

    public override void EndTurnChecks()
    {
        if (TimeLeft <= 1) {
            Target.ActiveEffectController.RemoveActiveEffect(this);
        }
    }
}
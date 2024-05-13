using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Gwen.Champion;

namespace Ashcrown.Remake.Core.Champions.Gwen.ActiveEffects;

public class CharmBuffActiveEffect : ActiveEffectBase
{
    public CharmBuffActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(GwenConstants.CharmBuffActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n" +
                           $"- {GwenConstants.Kiss.HighlightInGold()} can be used";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
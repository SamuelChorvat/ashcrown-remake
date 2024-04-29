using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Abstract;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;

namespace Ashcrown.Remake.Core.Champions.Eluard.ActiveEffects;

public class UnyieldingWillActiveEffect : ActiveEffectBase
{
    public UnyieldingWillActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(EluardConstants.UnyieldingWillActiveEffect, 
            originAbility, 
            championTarget)
    {
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        Duration1 = originAbility.Duration1;
        BonusDamage1 = originAbility.BonusDamage1;
        Description = $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
                           + $"- {EluardConstants.SwordStrike.HighlightInGold()} deals an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}\n"
                           + $"- {EluardConstants.Devastate.HighlightInGold()} can be used";
        Duration = Duration1;
        TimeLeft = Duration1;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;

namespace Ashcrown.Remake.Core.Champions.Althalos.ActiveEffects;

public class CrusaderOfLightActiveEffect : ActiveEffectBase
{
    public CrusaderOfLightActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(AlthalosConstants.CrusaderOfLightActiveEffect,
            originAbility,
            championTarget)
    {
        Duration1 = originAbility.Duration1;
        BonusDamage1 = originAbility.BonusDamage1;
        ReceiveDamageReductionPoint1 = originAbility.ReceiveDamageReductionPoint1;
        Description =
            $"- This champion has {$"{ReceiveDamageReductionPoint1} points of damage reduction".HighlightInYellow()}\n"
            + $"- This champion {"ignores stun".HighlightInPurple()} effects\n"
            + $"- {AlthalosConstants.HammerOfJustice.HighlightInGold()} deals an additional {$"{BonusDamage1} physical damage".HighlightInOrange()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        IgnoreStuns = originAbility.IgnoreStuns;
        AllDamageReceiveModifier = new PointsPercentageModifier(-ReceiveDamageReductionPoint1);
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
    }
}
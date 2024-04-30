using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Ability.Interfaces;
using Ashcrown.Remake.Core.Ability.Models;
using Ashcrown.Remake.Core.ActiveEffect.Base;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.ActiveEffects;

public class PrayerOfHealingMeActiveEffect : ActiveEffectBase
{
    public PrayerOfHealingMeActiveEffect(IAbility originAbility, IChampion championTarget) 
        : base(ArabelaConstants.PrayerOfHealingMeActiveEffect, originAbility, championTarget)
    {
        Duration1 = originAbility.Duration1;
        DealHealIncreasePoint1 = originAbility.DealHealIncreasePoint1;
        Description = $"- All healing done by this champion is increased by {$"{DealHealIncreasePoint1} health points".HighlightInGreen()}";
        Duration = Duration1;
        TimeLeft = Duration1;
        Helpful = originAbility.Helpful;
        Buff = originAbility.Buff;
        HealingDealModifier = new PointsPercentageModifier(DealHealIncreasePoint1);
    }
}
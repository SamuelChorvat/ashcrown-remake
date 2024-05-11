using Ashcrown.Remake.Core.Ability.Base;
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Ability.Extensions;
using Ashcrown.Remake.Core.Champion.Interfaces;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;

namespace Ashcrown.Remake.Core.Champions.Arabela.Abilities;

public class PrayerOfHealing : AbilityBase
{
    public PrayerOfHealing(IChampion champion)
        : base(champion,
            ArabelaConstants.PrayerOfHealing,
            3,
            [0,0,0,0,2],
            [AbilityClass.Strategic, AbilityClass.Instant],
            AbilityTarget.Ally,
            AbilityType.AllyBuff,
            3)
    {
        Heal1 = 10;
        Duration1 = 3;
        DealHealIncreasePoint1 = 10;
        Description = $"{ArabelaConstants.Name} prays for an ally. " +
                      $"For {Duration1} turns, that ally will heal {$"{Heal1} health".HighlightInGreen()} at the end of their turn. " +
                      $"For {Duration1} turns, any healing done by {ArabelaConstants.Name} will be increased by {$"{DealHealIncreasePoint1} health".HighlightInGreen()}.";
        SelfDisplay = true;
        Helpful = true;
        Buff = true;
        Healing = true;
        ActiveEffectOwner = ArabelaConstants.Name;
        ActiveEffectName = ArabelaConstants.PrayerOfHealingTargetActiveEffect;
    }

    public override int CalculateTotalPointsForTarget<T>(IChampion target)
    {
        var totalPoints = T.GetHealingPoints(Heal1, Duration1, this, target);
        totalPoints += 30;
        return totalPoints;
    }
}
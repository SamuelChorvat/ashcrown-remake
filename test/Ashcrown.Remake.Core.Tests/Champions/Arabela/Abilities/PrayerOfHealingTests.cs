using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Arabela.Abilities;

public class PrayerOfHealingTests
{
    [Fact]
    public void PrayerOfHealingAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 0, 2]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, [0, 0, 0, 2]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var playerInfo = battleLogic.GetBattlePlayer(1);
        playerInfo.Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.PrayerOfHealingMeActiveEffect).Should().BeTrue();
        playerInfo.Champions[0].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.PrayerOfHealingMeActiveEffect)!.TimeLeft.Should().Be(3);
        playerInfo.Champions[0].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.PrayerOfHealingMeActiveEffect)!.HealingDealModifier.Points.Should().Be(10);

        playerInfo.Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.PrayerOfHealingTargetActiveEffect).Should().BeTrue();
        playerInfo.Champions[1].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.PrayerOfHealingTargetActiveEffect)!.TimeLeft.Should().Be(3);
        playerInfo.Champions[1].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.PrayerOfHealingTargetActiveEffect)!.Heal1.Should().Be(10);
    }

    [Fact]
    public void PrayerOfHealingHealsCorrectAmount()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var usePrayerOfHealing = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 0, 2]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 70;

        // Act
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(70);
        battleLogic.AbilitiesUsed(1, usePrayerOfHealing, usePrayerOfHealing.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(90);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.PrayerOfHealingTargetActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.PrayerOfHealingTargetActiveEffect)!.TimeLeft.Should().Be(3);
    }

    [Fact]
    public void PrayerOfHealingIncreasesHealing()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var usePrayerOfHealing = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 1, 0, 0, 0], [0, 0, 0, 2]);
        var useFlashHeal = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 1, 0, 0, 0, 0],[1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 40;

        // Act
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(40);
        battleLogic.AbilitiesUsed(1, usePrayerOfHealing, [2, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useFlashHeal, [1, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(75);
    }
}
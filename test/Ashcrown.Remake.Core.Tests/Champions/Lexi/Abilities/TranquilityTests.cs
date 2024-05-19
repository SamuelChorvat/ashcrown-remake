using Ashcrown.Remake.Core.Champions.Fae.Champion;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lexi.Abilities;

public class TranquilityTests
{
    [Fact]
    public void TranquilityAppliesCorrectAeAndReducesTranquilityCost()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useTranquility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,0,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LexiConstants.TranquilityActiveEffect).Should().BeTrue();
        var tranquilityAe = champion.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        tranquilityAe!.Infinite.Should().BeTrue();
        tranquilityAe.Stacks.Should().Be(3);
        champion.AbilityController.GetCurrentAbility(3).GetTotalCurrentCost().Should().Be(0);
    }

    [Fact]
    public void TranquilityStackIsUsedWhenThornWhipIsUsed()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useTranquility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,0,1]);
        var useThornWhip = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,1,0], [0,0,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var result = battleLogic.AbilitiesUsed(1, useThornWhip, useThornWhip.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var tranquilityAe = champion.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        tranquilityAe!.Infinite.Should().BeTrue();
        tranquilityAe.Stacks.Should().Be(2);
    }

    [Fact]
    public void TranquilityStackIsUsedWhenNourishIsUsed()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useTranquility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,0,1]);
        var useNourish = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1,0,0,0,0,0], [0,0,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var result = battleLogic.AbilitiesUsed(1, useNourish, useNourish.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var tranquilityAe = champion.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        tranquilityAe!.Infinite.Should().BeTrue();
        tranquilityAe.Stacks.Should().Be(2);
    }

    [Fact]
    public void TranquilityHealsRemovesHarmfulAndUsesStackIfUsedWhileTranquilityIsActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(LexiConstants.Name, 
            FaeConstants.Name);
        var useCorruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useTranquility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 30;

        // Act
        battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 5);
        battleLogic.AbilitiesUsed(2, useCorruption, useCorruption.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(FaeConstants.CorruptionActiveEffect).Should().BeTrue();
        var result = battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!);
        result.Should().BeTrue();

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.Health.Should().Be(45);
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(FaeConstants.CorruptionActiveEffect).Should().BeFalse();
        var tranquilityAe = champion.ActiveEffectController.GetActiveEffectByName(LexiConstants.TranquilityActiveEffect);
        tranquilityAe!.Stacks.Should().Be(2);
    }
}
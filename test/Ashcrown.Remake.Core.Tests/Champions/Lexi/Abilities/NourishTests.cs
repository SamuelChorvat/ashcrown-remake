using Ashcrown.Remake.Core.Champions.Fae.Champion;
using Ashcrown.Remake.Core.Champions.Lexi.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lexi.Abilities;

public class NourishTests
{
    [Fact]
    public void NourishHealsCorrectAmount()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useNourish = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1,0,0,0,0,0], [1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var result = battleLogic.AbilitiesUsed(1, useNourish, useNourish.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(55);
    }

    [Fact]
    public void NourishRemovesAffliction()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(LexiConstants.Name, 
            FaeConstants.Name);
        var useCorruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useNourish = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1,0,0,0,0,0], [1,0,0,0]);

        // Act
        battleLogic.AbilitiesUsed(2, useCorruption, useCorruption.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(FaeConstants.CorruptionActiveEffect).Should().BeTrue();
        var result = battleLogic.AbilitiesUsed(1, useNourish, useNourish.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(FaeConstants.CorruptionActiveEffect).Should().BeFalse();
    }

    [Fact]
    public void NourishCostAndCooldownIsReducedDuringTranquility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useTranquility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,0,1]);
        
        var currentAbility = battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(2);
        currentAbility.GetCurrentCost()[0].Should().Be(1);
        currentAbility.GetTotalCurrentCost().Should().Be(1);
        currentAbility.GetCurrentCooldown().Should().Be(1);

        // Act
        battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        currentAbility.GetCurrentCost()[4].Should().Be(1);
        currentAbility.GetTotalCurrentCost().Should().Be(1);
        currentAbility.GetCurrentCooldown().Should().Be(0);
    }
}
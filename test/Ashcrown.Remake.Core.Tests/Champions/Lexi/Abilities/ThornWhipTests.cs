using Ashcrown.Remake.Core.Champions.Lexi.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lexi.Abilities;

public class ThornWhipTests
{
    [Fact]
    public void ThornWhipDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useThornWhip = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,1,0], [0,0,0,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useThornWhip, useThornWhip.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        enemyChampions[0].Health.Should().Be(100);
        enemyChampions[1].Health.Should().Be(75);
        enemyChampions[2].Health.Should().Be(100);
    }

    [Fact]
    public void ThornWhipDealsBonusDamageAndDamagesSecondaryTargetsDuringTranquility1()
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
        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        enemyChampions[0].Health.Should().Be(90);
        enemyChampions[1].Health.Should().Be(65);
        enemyChampions[2].Health.Should().Be(90);
    }

    [Fact]
    public void ThornWhipDealsBonusDamageAndDamagesSecondaryTargetsDuringTranquility2()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        var useTranquility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,0,1]);
        var useThornWhip = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,0,1], [0,0,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useTranquility, useTranquility.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var result = battleLogic.AbilitiesUsed(1, useThornWhip, useThornWhip.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        enemyChampions[0].Health.Should().Be(90);
        enemyChampions[1].Health.Should().Be(90);
        enemyChampions[2].Health.Should().Be(65);
    }
}
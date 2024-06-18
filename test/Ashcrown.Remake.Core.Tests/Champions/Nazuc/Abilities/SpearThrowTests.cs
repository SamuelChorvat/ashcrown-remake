using Ashcrown.Remake.Core.Champions.Nazuc.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nazuc.Abilities;

public class SpearThrowTests
{
    [Fact]
    public void SpearThrowDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        var useSpearThrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useSpearThrow, useSpearThrow.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
    }

    [Fact]
    public void SpearThrowDealsBonusDamageToTargetAffectedByHuntersMark()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        var useHuntersMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [0,0,0,1,0,0], [0,1,0,0]);
        var useSpearThrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        var resultHuntersMark = battleLogic.AbilitiesUsed(1, useHuntersMark, useHuntersMark.SpentEnergy!);
        resultHuntersMark.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        var resultSpearThrow = battleLogic.AbilitiesUsed(1, useSpearThrow, useSpearThrow.SpentEnergy!);
        resultSpearThrow.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(65);
    }
}
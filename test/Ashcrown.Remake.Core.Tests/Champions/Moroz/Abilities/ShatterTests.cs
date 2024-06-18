using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Moroz.Abilities;

public class ShatterTests
{
    [Fact]
    public void ShatterKillsTargetAndIsReplacedByFreeze()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        var useFreeze = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,1,0,0]);
        var useShatter = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [2,0,0,0]);

        // Act
        var resultFreeze = battleLogic.AbilitiesUsed(1, useFreeze, useFreeze.SpentEnergy!);
        resultFreeze.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var resultShatter = battleLogic.AbilitiesUsed(1, useShatter, useShatter.SpentEnergy!);
        resultShatter.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(0);
        enemyChampion.Alive.Should().BeFalse();
        champion.AbilityController.GetCurrentAbility(1).Name.Should().Be(MorozConstants.Freeze);
    }
}
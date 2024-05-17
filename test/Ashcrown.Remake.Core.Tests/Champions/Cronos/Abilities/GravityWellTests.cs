using Ashcrown.Remake.Core.Champions.Cronos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cronos.Abilities;

public class GravityWellTests
{
    [Fact]
    public void GravityWellDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        var useGravityWell = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useGravityWell, useGravityWell.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        battleLogic.GetBattlePlayer(2).Champions[1].Health.Should().Be(90);
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(90);
    }
  
}
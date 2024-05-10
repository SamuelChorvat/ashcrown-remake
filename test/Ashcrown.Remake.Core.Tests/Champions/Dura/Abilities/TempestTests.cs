using Ashcrown.Remake.Core.Champions.Dura.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Dura.Abilities;

public class TempestTests
{
    [Fact]
    public void TempestDealsCorrectDamageAndRemovesEnergy()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DuraConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 0, 0], [1, 0, 1, 0]);

        // Act
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(40);
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();

        // Assert
        var champion2 = battleLogic.GetBattlePlayer(2).Champions[0];
        champion2.Health.Should().Be(60);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(39);
    }

}
using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jane.Abilities;

public class GoForTheThroatTests
{
    [Fact]
    public void GoForTheThroatDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        var useBenji = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,1,0,0]);
        var useGoForTheThroat = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,0,1,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useBenji, useBenji.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var result = battleLogic.AbilitiesUsed(1, useGoForTheThroat, useGoForTheThroat.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(75);
    }
}
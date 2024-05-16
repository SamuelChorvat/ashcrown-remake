using Ashcrown.Remake.Core.Champions.Hannibal.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hannibal.Abilities;

public class TasteForBloodTests
{
    [Fact]
    public void TasteForBloodDealsCorrectDamageAndHeals()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(10);
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(45);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(65);
    }

    [Fact]
    public void TasteForBloodOnlyHealsHealthLost()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;
        battleLogic.GetBattlePlayer(2).Champions[0].Health = 15;

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(10);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(15);
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(25);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(0);
    }
}
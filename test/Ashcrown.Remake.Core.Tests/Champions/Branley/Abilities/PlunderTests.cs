using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Branley.Abilities;

public class PlunderTests
{
    [Fact]
    public void PlunderDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 1, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
    }
    
    [Fact]
    public void PlunderDealsBonusDamageAndIgnoresInvulnerabilityDuringRaiseTheFlag()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        var useRaiseTheFlag = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [1, 0, 0, 0]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1, 0, 0, 0, 0, 0], [2, 0, 0, 0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 1, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useRaiseTheFlag, useRaiseTheFlag.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.AbilitiesUsed(1, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(55);
    }
}
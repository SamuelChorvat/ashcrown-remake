using Ashcrown.Remake.Core.Champions.Izrin.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Izrin.Abilities;

public class BiteTests
{
    [Fact]
    public void BiteDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
    }

    [Fact]
    public void BiteDealsBonusDamageForMissingHealth()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 60;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }

    [Fact]
    public void BiteDealsBonusDamageForDeadAllies()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 0;
        battleLogic.GetBattlePlayer(1).Champions[1].Alive = false;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }

    [Fact]
    public void BiteDealsBonusDamageForDeadAlliesAndMissingHealth()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,1,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 0;
        battleLogic.GetBattlePlayer(1).Champions[1].Alive = false;
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 60;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(40);
    }
}
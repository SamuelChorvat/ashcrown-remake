using Ashcrown.Remake.Core.Champions.Cleo.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cleo.Abilities;

public class HiveTests
{
    [Fact]
    public void HiveHealsCorrectAmountAndAppliesCorrectActiveEffectAndIsReplaced()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useHive = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 1, 0, 1]);
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health = 10;
        }

        // Act
        battleLogic.AbilitiesUsed(1, useHive, useHive.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health.Should().Be(50);
            var ae = champ.ActiveEffectController.GetActiveEffectByName(CleoConstants.HiveActiveEffect);
            ae.Should().NotBeNull();
            ae!.Infinite.Should().BeTrue();
        }
    }
    
    [Fact]
    public void HiveAeHealsAtTheStartOfTheTurn()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useHive = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 1, 0, 1]);
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health = 10;
        }

        // Act
        battleLogic.AbilitiesUsed(1, useHive, useHive.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health.Should().Be(55);
            var ae = champ.ActiveEffectController.GetActiveEffectByName(CleoConstants.HiveActiveEffect);
            ae.Should().NotBeNull();
            ae!.Infinite.Should().BeTrue();
        }
    }
    
    [Fact]
    public void HiveEndsIfCleoIsKilled()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useHive = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 1, 0, 1]);
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health = 10;
        }

        // Act
        battleLogic.AbilitiesUsed(1, useHive, useHive.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].ChampionController.OnDeath();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Alive.Should().BeFalse();
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.ActiveEffectController.GetActiveEffectByName(CleoConstants.HiveActiveEffect).Should().BeNull();
        }
    }



}
using Ashcrown.Remake.Core.Champions.Cleo.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cleo.Abilities;

public class HealingSalveTests
{
    [Fact]
    public void HealingSalveHealsCorrectAmountAndAppliesCorrectActiveEffectAndIsReplaced()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useHive = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 1, 0, 1]);
        var useHealingSalve = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 0, 0, 0]);
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health = 10;
        }

        // Act
        battleLogic.AbilitiesUsed(1, useHive, useHive.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useHealingSalve, useHealingSalve.SpentEnergy!).Should().BeTrue();

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health.Should().Be(65);
            var ae = champ.ActiveEffectController.GetActiveEffectByName(CleoConstants.HealingSalveActiveEffect);
            ae.Should().NotBeNull();
            ae!.Infinite.Should().BeTrue();
            ae.DestructibleDefense.Should().Be(10);
        }
    }
    
    [Fact]
    public void HealingSalveStacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useHive = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 1, 0, 1]);
        var useHealingSalve = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [1, 0, 0, 0]);
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            champ.Health = 10;
        }
        // Act
        battleLogic.AbilitiesUsed(1, useHive, useHive.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useHealingSalve, useHealingSalve.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);
        battleLogic.AbilitiesUsed(1, useHealingSalve, useHealingSalve.SpentEnergy!).Should().BeTrue();

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            var ae = champ.ActiveEffectController.GetActiveEffectByName(CleoConstants.HealingSalveActiveEffect);
            ae!.DestructibleDefense.Should().Be(20);
        }
    }
}
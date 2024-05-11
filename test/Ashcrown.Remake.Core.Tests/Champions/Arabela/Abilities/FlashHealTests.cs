using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Arabela.Abilities;

public class FlashHealTests
{
    [Fact]
    public void FlashHealHealsTheCorrectAmountAndRemovesAfflictions()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var useFlashHeal = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [1,0,0,0]);
        var useAffliction = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1,
            [0,0,0,1,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 60;
        battleLogic.GetBattlePlayer(2).Champions[0]
            .CurrentAbilities[0].AbilityClasses = [AbilityClass.Affliction];

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(60);
        battleLogic.AbilitiesUsed(2, useAffliction, useAffliction.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapTargetActiveEffect).Should().BeTrue();
        battleLogic.AbilitiesUsed(1, useFlashHeal, [1, 0, 0, 0]).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapTargetActiveEffect).Should().BeFalse();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapTargetActiveEffect).Should().BeFalse();
    }
    
    [Fact]
    public void FlashHealHealsTheCorrectAmountAndNotRemoveNotAfflictions()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var useFlashHeal = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [1,0,0,0]);
        var useNonAffliction = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1,
            [0,0,0,1,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 60;
        battleLogic.GetBattlePlayer(2).Champions[0]
            .CurrentAbilities[0].AbilityClasses = [AbilityClass.Magic];

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(60);
        battleLogic.AbilitiesUsed(2, useNonAffliction, useNonAffliction.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapTargetActiveEffect).Should().BeTrue();
        battleLogic.AbilitiesUsed(1, useFlashHeal, [1, 0, 0, 0]).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapTargetActiveEffect).Should().BeTrue();
    }
    
    [Fact]
    public void FlashHealDoesNotOverHeal()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var useFlashHeal = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 99;

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(99);
        battleLogic.AbilitiesUsed(1, useFlashHeal, [1, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(100);
    }
    
    [Fact]
    public void FlashHealDoesNotResurrect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var useFlashHeal = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 0;
        battleLogic.GetBattlePlayer(1).Champions[0].Alive = false;

        // Act & Assert
        battleLogic.AbilitiesUsed(1, useFlashHeal, [1, 0, 0, 0]).Should().BeFalse();
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(0);
        battleLogic.GetBattlePlayer(1).Champions[0].Alive.Should().BeFalse();
    }
}
using Ashcrown.Remake.Core.Champions.Braya.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Braya.Abilities;

public class HuntersFocusTests
{
    [Fact]
    public void HuntersFocusAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        var useHuntersFocus = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useHuntersFocus, useHuntersFocus.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.Stacks.Should().Be(4);
    }
    
    [Fact]
    public void HuntersFocusAppliesCorrectActiveEffectIfUsedWhileActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        var useHuntersFocus = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useHuntersFocus, useHuntersFocus.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 4);
        battleLogic.AbilitiesUsed(1, useHuntersFocus, useHuntersFocus.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var stacksAe = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        stacksAe.Should().NotBeNull();
        stacksAe!.Infinite.Should().BeTrue();
        stacksAe.Stacks.Should().Be(3);

        var ignoreAe = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BrayaConstants.HuntersFocusIgnoreActiveEffect);
        ignoreAe.Should().NotBeNull();
        ignoreAe!.TimeLeft.Should().Be(1);
        ignoreAe.IgnoreHarmful.Should().BeTrue();
    }
}
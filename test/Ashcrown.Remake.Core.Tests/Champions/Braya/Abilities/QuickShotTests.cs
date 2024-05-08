using Ashcrown.Remake.Core.Champions.Braya.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Braya.Abilities;

public class QuickShotTests
{
    [Fact]
    public void QuickShotIsNotUsableIfHuntersFocusIsNotActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        var useQuickShot = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);
        
        // Act
        var act = () => battleLogic.AbilitiesUsed(1, useQuickShot, useQuickShot.SpentEnergy!);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }
    
    [Fact]
    public void QuickShotDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        var useHuntersFocus = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useQuickShot = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 1, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useHuntersFocus, useHuntersFocus.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useQuickShot, useQuickShot.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        var aeHuntersFocus = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BrayaConstants.HuntersFocusStacksActiveEffect);
        aeHuntersFocus.Should().NotBeNull();
        aeHuntersFocus?.Stacks.Should().Be(3);

        var aeQuickShot = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BrayaConstants.QuickShotActiveEffect);
        aeQuickShot.Should().NotBeNull();
        aeQuickShot?.TimeLeft.Should().Be(1);
        aeQuickShot?.Invulnerability.Should().BeTrue();
    }

}
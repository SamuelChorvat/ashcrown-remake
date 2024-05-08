using Ashcrown.Remake.Core.Champions.Braya.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Braya.Abilities;

public class KillShotTests
{
    [Fact]
    public void KillShotIsNotUsableIfHuntersFocusIsNotActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        var useKillShot = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);
        
        // Act
        var act = () => battleLogic.AbilitiesUsed(1, useKillShot, useKillShot.SpentEnergy!);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }
    
    [Fact]
    public void KillShotDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        var useHuntersFocus = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useKillShot = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useHuntersFocus, useHuntersFocus.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useKillShot, useKillShot.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(40);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(BrayaConstants.HuntersFocusStacksActiveEffect).Should().BeFalse();
    }
}
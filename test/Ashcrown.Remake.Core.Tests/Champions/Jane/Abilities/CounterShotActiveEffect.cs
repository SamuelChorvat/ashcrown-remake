using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jane.Abilities;

public class CounterShotActiveEffect
{
    [Fact]
    public void CounterShotDealsCorrectDamageAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        var useCounterShot = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [0,0,1,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useCounterShot, useCounterShot.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(70);
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JaneConstants.CounterShotActiveEffect).Should().BeTrue();
        var counterShotAe = enemyChampion.ActiveEffectController.GetActiveEffectByName(JaneConstants.CounterShotActiveEffect);
        counterShotAe!.TimeLeft.Should().Be(2);
    }

    [Fact]
    public void CounterShotCorrectlyReducesStunDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(JaneConstants.Name, 
            AlthalosConstants.Name);
        var useCounterShot = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [0,0,1,1]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,0,1,0]);

        // Act
        var counterShotResult = battleLogic.AbilitiesUsed(1, useCounterShot, useCounterShot.SpentEnergy!);
        counterShotResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var hammerOfJusticeResult = battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!);
        hammerOfJusticeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var allyChampion = battleLogic.GetBattlePlayer(1).Champions[0];
        allyChampion.Health.Should().Be(80);
        allyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AlthalosConstants.HammerOfJusticeActiveEffect).Should().BeFalse();
    }
}
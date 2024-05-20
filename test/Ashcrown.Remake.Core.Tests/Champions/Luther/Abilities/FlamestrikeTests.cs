using Ashcrown.Remake.Core.Champions.Luther.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Luther.Abilities;

public class FlamestrikeTests
{
    [Fact]
    public void FlamestrikeAppliesCorrectAesAndDealsDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useFlameStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [1,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useFlameStrike, useFlameStrike.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];

        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.FlamestrikeMeActiveEffect).Should().BeTrue();
        enemyChampion.Health.Should().Be(80);
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.FlamestrikeTargetActiveEffect).Should().BeTrue();
        var flamestrikeTargetAE = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.FlamestrikeTargetActiveEffect);
        flamestrikeTargetAE!.TimeLeft.Should().Be(1);
        flamestrikeTargetAE.Damage1.Should().Be(20);
    }

    [Fact]
    public void FlamestrikeIncreasesCosts()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useFlameStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [1,1,0,0]);

        // Assert initial cost
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.AbilityController.GetCurrentAbility(4).GetCurrentCost()[4].Should().Be(1);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useFlameStrike, useFlameStrike.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert increased cost
        enemyChampion.AbilityController.GetCurrentAbility(4).GetCurrentCost()[4].Should().Be(2);
    }
}
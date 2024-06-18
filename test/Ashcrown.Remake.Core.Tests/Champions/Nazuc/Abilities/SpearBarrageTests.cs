using Ashcrown.Remake.Core.Champions.Nazuc.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nazuc.Abilities;

public class SpearBarrageTests
{
    [Fact]
    public void SpearBarrageAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        var useSpearBarrage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0,0,0,1,1,1], [0,1,1,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useSpearBarrage, useSpearBarrage.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(NazucConstants.SpearBarrageMeActiveEffect).Should().BeTrue();
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(NazucConstants.SpearBarrageBuffActiveEffect).Should().BeTrue();
        var aeBuff = champion.ActiveEffectController.GetActiveEffectByName(NazucConstants.SpearBarrageBuffActiveEffect);
        aeBuff!.TimeLeft.Should().Be(3);
        aeBuff.AllDamageReceiveModifier.Points.Should().Be(-15);

        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        foreach (var enemyChampion in enemyChampions)
        {
            enemyChampion.Health.Should().Be(85);
            enemyChampion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(NazucConstants.SpearBarrageTargetActiveEffect).Should().BeTrue();
            var aeTarget = enemyChampion.ActiveEffectController.GetActiveEffectByName(NazucConstants.SpearBarrageTargetActiveEffect);
            aeTarget!.TimeLeft.Should().Be(2);
            aeTarget.Damage1.Should().Be(15);
        }
    }

    [Fact]
    public void SpearBarrageReducesCostOfSpearThrow()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        var useSpearBarrage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0,0,0,1,1,1], [0,1,1,0]);
        var ability = battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(1);
        ability.GetCurrentCost()[4].Should().Be(1);
        ability.GetTotalCurrentCost().Should().Be(2);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useSpearBarrage, useSpearBarrage.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        ability.GetCurrentCost()[4].Should().Be(0);
        ability.GetTotalCurrentCost().Should().Be(1);
    }
}
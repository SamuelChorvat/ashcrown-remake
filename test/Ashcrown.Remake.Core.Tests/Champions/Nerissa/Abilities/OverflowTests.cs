using Ashcrown.Remake.Core.Ai;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Champions.Nerissa.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nerissa.Abilities;

public class OverflowTests
{
    [Fact]
    public void OverflowDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        var useOverflow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useOverflow, useOverflow.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
    }

    [Fact]
    public void OverflowDealsBonusDamageToTargetAffectedByDrown()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        var useDrown = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0,0,0,1,0,0], [1,0,0,0]);
        var useOverflow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useDrown, useDrown.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        result = battleLogic.AbilitiesUsed(1, useOverflow, useOverflow.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }

    [Fact]
    public void OverflowRemovesDestructibleDefense()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(NerissaConstants.Name, 
            MorozConstants.Name);
        var useEnemyDestructibleDefense = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed( 
            2, 3, [0,1,0,0,0,0], [1,0,0,0]);
        var useOverflow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,0,1,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(2, useEnemyDestructibleDefense, useEnemyDestructibleDefense.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        AiPointsCalculator.GetTotalDestructible(battleLogic.GetBattlePlayer(2).Champions[1]).Should().Be(40);
        battleLogic.GetBattlePlayer(2).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FrozenArmorActiveEffect).Should().BeTrue();
        result = battleLogic.AbilitiesUsed(1, useOverflow, useOverflow.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FrozenArmorActiveEffect).Should().BeFalse();
        AiPointsCalculator.GetTotalDestructible(battleLogic.GetBattlePlayer(2).Champions[1]).Should().Be(0);
        battleLogic.GetBattlePlayer(2).Champions[1].Health.Should().Be(85);
    }
}
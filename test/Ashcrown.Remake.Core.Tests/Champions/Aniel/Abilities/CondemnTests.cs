using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Aniel.Abilities;

public class CondemnTests
{
    [Fact]
    public void CondemnShouldDealCorrectDamageAndApplyActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useCondemn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useCondemn, useCondemn.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnUsedActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.CondemnUsedActiveEffect)!.TimeLeft.Should().Be(1);
    }

    [Fact]
    public void CondemnShouldDealBonusDamageIfUsedOnEnemyAffectedByBladeOfGluttony()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useBladeOfGluttony = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        var useCondemn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useBladeOfGluttony, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useCondemn, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }

    [Fact]
    public void CondemnShouldMakeAnielInvulnerableIfUsedAfterEnchantedGarlicBomb()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useEnchantedGarlicBomb = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        var useCondemn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useEnchantedGarlicBomb, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useCondemn, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnUsedActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.CondemnUsedActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.CondemnInvulnerabilityActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.CondemnInvulnerabilityActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.CondemnInvulnerabilityActiveEffect)!.Invulnerability.Should().BeTrue();
    }
}
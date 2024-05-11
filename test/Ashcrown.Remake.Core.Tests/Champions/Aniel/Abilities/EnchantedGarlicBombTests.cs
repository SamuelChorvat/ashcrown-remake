using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Aniel.Abilities;

public class EnchantedGarlicBombTests
{
    [Fact]
    public void EnchantedGarlicBombCorrectlyAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useEnchantedGarlicBomb = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useEnchantedGarlicBomb, useEnchantedGarlicBomb.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.TimeLeft.Should().Be(2);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.DisableDamageReceiveReduction.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.DisableInvulnerability.Should().BeTrue();
    }

    [Fact]
    public void EnchantedGarlicBombWillAlsoStunIfUsedOnTargetAffectedByBladeOfGluttony()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useBladeOfGluttony = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        var useEnchantedGarlicBomb = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useBladeOfGluttony, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useEnchantedGarlicBomb, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.TimeLeft.Should().Be(2);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedTargetStunActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetStunActiveEffect)!.Stun.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetStunActiveEffect)!.TimeLeft.Should().Be(1);
    }

    [Fact]
    public void EnchantedGarlicBombWillDealCorrectDamageIfUsedOnTargetAffectedByCondemn()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useCondemn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        var useEnchantedGarlicBomb = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useCondemn, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        battleLogic.AbilitiesUsed(1, useEnchantedGarlicBomb, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(75);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.TimeLeft.Should().Be(2);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.DisableDamageReceiveReduction.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.EnchantedGarlicBombUsedTargetCantActiveEffect)!.DisableInvulnerability.Should().BeTrue();
    }
}
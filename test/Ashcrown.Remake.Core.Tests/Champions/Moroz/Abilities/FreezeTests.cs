using Ashcrown.Remake.Core.Champions.Dex.Champion;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Moroz.Abilities;

public class FreezeTests
{
    [Fact]
    public void FreezeAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        var useFreeze = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,1,0,0]);
        
        // Act
        var result = battleLogic.AbilitiesUsed(1, useFreeze, useFreeze.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var targetChampion = battleLogic.GetBattlePlayer(2).Champions[0];

        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeMeActiveEffect).Should().BeTrue();
        targetChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeTargetActiveEffect).Should().BeTrue();
        var freezeTargetAe = targetChampion.ActiveEffectController
            .GetActiveEffectByName(MorozConstants.FreezeTargetActiveEffect);
        freezeTargetAe!.Stun.Should().BeTrue();
        freezeTargetAe.DisableDamageReceiveReduction.Should().BeTrue();
        freezeTargetAe.DisableInvulnerability.Should().BeTrue();
        freezeTargetAe.StunType!.Length.Should().Be(3);
        freezeTargetAe.TimeLeft.Should().Be(2);
    }

    [Fact]
    public void FreezeEndsIfStunned()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(MorozConstants.Name, 
            DexConstants.Name);
        var useFreeze = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,1,0,0]);
        var useGarrote = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(3, 2, 
            [0,0,0,1,0,0], [0,2,0,0]);

        // Act
        var freezeResult = battleLogic.AbilitiesUsed(1, useFreeze, useFreeze.SpentEnergy!);
        freezeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var useGarroteResult = battleLogic.AbilitiesUsed(2, useGarrote, useGarrote.SpentEnergy!);
        useGarroteResult.Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeMeActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FreezeTargetActiveEffect).Should().BeFalse();
    }

    [Fact]
    public void FreezeIsReplacedByShatterAfterItIsUsed()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        var useFreeze = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,1,0,0]);
        
        // Act
        var result = battleLogic.AbilitiesUsed(1, useFreeze, useFreeze.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(1).Name.Should().Be("Shatter");
    }
}
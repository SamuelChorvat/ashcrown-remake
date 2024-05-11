using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Ash.Abilities;

public class FireShieldTests
{
    [Fact]
    public void FireShieldShouldApplyCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var useFireShield = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
    
        // Act
        battleLogic.AbilitiesUsed(1, useFireShield, useFireShield.SpentEnergy!).Should().BeTrue();
    
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireShieldMeActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireShieldTargetActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireShieldTargetActiveEffect)!.TimeLeft.Should().Be(3);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireShieldTargetActiveEffect)!.Damage1.Should().Be(10);
    }
    
    [Fact]
    public void FireShieldShouldDealCorrectDamageOverItsDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var useFireShield = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
    
        // Act
        battleLogic.AbilitiesUsed(1, useFireShield, useFireShield.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);
    
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireShieldTargetActiveEffect).Should().BeFalse();
    }

    [Fact]
    public void FireShieldShouldDealExtraDamageIfTargetedByPhysicalAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(AshConstants.Name, 
            AlthalosConstants.Name);
        var useFireShield = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);
    
        // Act
        battleLogic.AbilitiesUsed(1, useFireShield, [1, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
    
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
    }
    
    [Fact]
    public void FireShieldShouldNotDealExtraDamageIfTargetedByNonPhysicalAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var useFireShield = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        var useFireball = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);
    
        // Act
        battleLogic.AbilitiesUsed(1, useFireShield, useFireShield.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
        battleLogic.AbilitiesUsed(2, useFireball, useFireShield.SpentEnergy!).Should().BeFalse();
    
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
    }


}
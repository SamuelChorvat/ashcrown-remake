using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Althalos.Abilities;

public class HammerOfJusticeTests
{
    [Fact]
    public void HammerOfJusticeShouldDealCorrectDamageAndApplyActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AlthalosConstants.HammerOfJusticeActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AlthalosConstants.HammerOfJusticeActiveEffect)!.TimeLeft.Should().Be(1);
    }

    [Fact]
    public void HammerOfJusticeShouldDealCorrectBonusDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        var useCrusaderOfLight = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3,
            [1,0,0,0,0,0], [1,0,0,0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useCrusaderOfLight, useCrusaderOfLight.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
    }
    
    [Theory]
    [InlineData(new[] {AbilityClass.Physical})]
    [InlineData(new[] {AbilityClass.Strategic})]
    [InlineData(new[] {AbilityClass.Physical, AbilityClass.Strategic})]
    public void HammerOfJusticeShouldStunPhysicalAndStrategicAbilities(AbilityClass[] abilityClasses)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        battleLogic.GetBattlePlayer(2).Champions[0].CurrentAbilities[0].AbilityClasses = abilityClasses;
        
        // Act
        battleLogic.AbilitiesUsed(1, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var act = () => battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!);
        
        // Assert
        act.Should().Throw<Exception>().WithMessage("Stunned to use problem");
    }
    
    [Theory]
    [InlineData(new[] {AbilityClass.Magic})]
    [InlineData(new[] {AbilityClass.Affliction})]
    [InlineData(new[] {AbilityClass.Melee, AbilityClass.Control, AbilityClass.Action, AbilityClass.Ranged})]
    public void HammerOfJusticeShouldNotStunNonPhysicalAndStrategicAbilities(AbilityClass[] abilityClasses)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        battleLogic.GetBattlePlayer(2).Champions[0].CurrentAbilities[0].AbilityClasses = abilityClasses;
        
        // Act
        battleLogic.AbilitiesUsed(1, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
    }
}
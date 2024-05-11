using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Althalos.Abilities;

public class CrusaderOfLightTests
{
    [Fact]
    public void CrusaderOfLightShouldApplyCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var endTurn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1, 0, 0, 0, 0, 0], [1,0,0,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, endTurn, endTurn.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AlthalosConstants.CrusaderOfLightActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AlthalosConstants.CrusaderOfLightActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(1).Champions[2].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AlthalosConstants.CrusaderOfLightActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AlthalosConstants.CrusaderOfLightActiveEffect)!.IgnoreStuns.Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AlthalosConstants.CrusaderOfLightActiveEffect)!.TimeLeft.Should().Be(4);
    }

    [Fact]
    public void CrusaderOfLightShouldCorrectlyReduceDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var useCrusaderOfLight = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3,
            [1,0,0,0,0,0], [1,0,0,0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useCrusaderOfLight, useCrusaderOfLight.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(90);
    }

    [Fact]
    public void CrusaderOfLightShouldIgnoreStuns()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var useCrusaderOfLight = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3,
            [1,0,0,0,0,0], [1,0,0,0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useCrusaderOfLight, useCrusaderOfLight.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        
        // Assert
        battleLogic.AbilitiesUsed(1, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
    }
}
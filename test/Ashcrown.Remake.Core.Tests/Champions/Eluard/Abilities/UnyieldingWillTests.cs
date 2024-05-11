using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Eluard.Abilities;

public class UnyieldingWillTests
{
    [Fact]
    public void UnyieldingWillAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Name);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [1,0,0,0,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useUnyieldingWill, useUnyieldingWill.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(EluardConstants.UnyieldingWillActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.UnyieldingWillActiveEffect)!.TimeLeft.Should().Be(4);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.UnyieldingWillActiveEffect)!.AllDamageReceiveModifier.Points.Should()
            .Be(-15);
    }
    
    [Fact]
    public void UnyieldingWillCorrectlyReducesDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Name);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [1,0,0,0,0,0], [0,0,1,0]);
        var useSwordStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useUnyieldingWill, useUnyieldingWill.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useSwordStrike, useSwordStrike.SpentEnergy!).Should().BeTrue();
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(95);
    }
}
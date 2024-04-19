using Ashcrown.Remake.Core.Champions.Sarfu.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Sarfu.Abilities;

public class ChargeTests
{
    [Fact]
    public void ChargeDealsCorrectDamage() 
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(SarfuConstants.Sarfu);
        var useCharge = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,2, 
            [0,0,0,1,0,0], [0,1,0,1]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useCharge, useCharge.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(65);
    }
    
    [Fact]
    public void ChargeDealsBonusDamageToTargetAffectedByDuel()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(SarfuConstants.Sarfu);
        var useDuel = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [0,0,0,1,0,0], [0,0,1,0]);
        var useCharge = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,2, 
            [0,0,0,1,0,0], [0,1,0,1]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useDuel, useDuel.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useCharge, useCharge.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(40);
    }
}
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Eluard.Abilities;

public class SwordStrikeTests
{
    [Fact]
    public void SwordStrikeDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Eluard);
        var useSwordStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useSwordStrike, useSwordStrike.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
    }

    [Fact]
    public void SwordStrikeDealBonusDamageWhileUnyieldingWillIsActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Eluard);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [1,0,0,0,0,0], [0,0,1,0]);
        var useSwordStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useUnyieldingWill, useUnyieldingWill.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useSwordStrike, useSwordStrike.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
    }
}
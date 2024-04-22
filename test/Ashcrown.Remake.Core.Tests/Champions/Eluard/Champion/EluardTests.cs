using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Eluard.Champion;

public class EluardTests
{
    [Fact]
    public void EluardHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Eluard);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(EluardConstants.Eluard);
    }
    
    [Fact]
    public void EluardHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Eluard);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(EluardConstants.SwordStrike);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(EluardConstants.Devastate);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(EluardConstants.UnyieldingWill);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(EluardConstants.Evade);
    }
}
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Sarfu.Champion;

public class SarfuTests
{
    [Fact]
    public void EluardHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(SarfuConstants.Sarfu);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(SarfuConstants.Sarfu);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(SarfuConstants.Title);
    }
    
    [Fact]
    public void EluardHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(SarfuConstants.Sarfu);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(SarfuConstants.Overpower);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(SarfuConstants.Charge);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(SarfuConstants.Duel);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(SarfuConstants.Deflect);
    }
}
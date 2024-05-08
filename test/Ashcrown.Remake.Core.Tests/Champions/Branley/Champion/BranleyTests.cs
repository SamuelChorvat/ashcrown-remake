using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Branley.Champion;

public class BranleyTests
{
    [Fact]
    public void BranleyHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(BranleyConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(BranleyConstants.Title);
    }
    
    [Fact]
    public void BranleyHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(BranleyConstants.Plunder);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(BranleyConstants.FireTheCannons);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(BranleyConstants.RaiseTheFlag);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(BranleyConstants.DefensiveManeuver);
    }
}
using Ashcrown.Remake.Core.Champions.Braya.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Braya.Champion;

public class BrayaTests
{
    [Fact]
    public void BrayaHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(BrayaConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(BrayaConstants.Title);
    }
    
    [Fact]
    public void BrayaHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BrayaConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(BrayaConstants.HuntersFocus);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(BrayaConstants.QuickShot);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(BrayaConstants.KillShot);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(BrayaConstants.Disengage);
    }
}
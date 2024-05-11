using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cedric.Champion;

public class CedricTests
{
    [Fact]
    public void CedricHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(CedricConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(CedricConstants.Title);
    }
    
    [Fact]
    public void CedricHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(CedricConstants.DarkSoul);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(CedricConstants.MirrorImage);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(CedricConstants.TimeWarp);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(CedricConstants.NetherSlip);
    }
}
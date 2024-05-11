using Ashcrown.Remake.Core.Champions.Azrael.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Azrael.Champion;

public class AzraelTests
{
    [Fact]
    public void AzraelHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(AzraelConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(AzraelConstants.Title);
    }
    
    [Fact]
    public void AzraelHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(AzraelConstants.SoulRealm);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(AzraelConstants.Reap);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(AzraelConstants.CursedMark);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(AzraelConstants.Disappear);
    }
}
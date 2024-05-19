using Ashcrown.Remake.Core.Champions.Khan.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Khan.Champion;

public class KhanTests
{
    [Fact]
    public void KhanHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(KhanConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(KhanConstants.Title);
    }
    
    [Fact]
    public void KhanHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(KhanConstants.MortalStrike);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(KhanConstants.Bladestorm);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(KhanConstants.HandOfTheProtector);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(KhanConstants.Perception);
    }
}
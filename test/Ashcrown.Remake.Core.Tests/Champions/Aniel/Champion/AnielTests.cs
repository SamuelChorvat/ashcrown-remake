using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Aniel.Champion;

public class AnielTests
{
    [Fact]
    public void AnielHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(AnielConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(AnielConstants.Title);
    }
    
    [Fact]
    public void AnielHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(AnielConstants.Condemn);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(AnielConstants.EnchantedGarlicBomb);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(AnielConstants.BladeOfGluttony);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(AnielConstants.AdrenalineRush);
    }
}
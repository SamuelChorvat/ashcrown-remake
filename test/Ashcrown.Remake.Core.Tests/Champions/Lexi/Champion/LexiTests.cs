using Ashcrown.Remake.Core.Champions.Lexi.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lexi.Champion;

public class LexiTests
{
    [Fact]
    public void LexiHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(LexiConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(LexiConstants.Title);
    }
    
    [Fact]
    public void LexiHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LexiConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(LexiConstants.ThornWhip);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(LexiConstants.Nourish);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(LexiConstants.Tranquility);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(LexiConstants.Shadowmeld);
    }
}
using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jane.Champion;

public class JaneTests
{
    [Fact]
    public void JaneHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(JaneConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(JaneConstants.Title);
    }
    
    [Fact]
    public void JaneHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(JaneConstants.Benji);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[0][1].Name.Should().Be(JaneConstants.GoForTheThroat);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(JaneConstants.CounterShot);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(JaneConstants.Flashbang);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(JaneConstants.Misdirection);
    }
}
using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jafali.Champion;

public class JafaliTests
{
    [Fact]
    public void JafaliHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(JafaliConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(JafaliConstants.Title);
    }
    
    [Fact]
    public void JafaliHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(JafaliConstants.Anger);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[0][1].Name.Should().Be(JafaliConstants.DecayingSoul);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(JafaliConstants.Envy);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[1][1].Name.Should().Be(JafaliConstants.Avarice);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(JafaliConstants.Pride);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(JafaliConstants.DevilsGame);
    }
}
using Ashcrown.Remake.Core.Champions.Cleo.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cleo.Champion;

public class CleoTests
{
    [Fact]
    public void CleoHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(CleoConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(CleoConstants.Title);
    }
    
    [Fact]
    public void CleoHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(CleoConstants.VenomousSting);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(CleoConstants.ChitinRegeneration);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(CleoConstants.Hive);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[2][1].Name.Should().Be(CleoConstants.HealingSalve);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(CleoConstants.SwarmSummoning);
    }
}
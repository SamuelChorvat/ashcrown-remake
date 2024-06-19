using Ashcrown.Remake.Core.Champions.Nerissa.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nerissa.Champion;

public class NerissaTests
{
    [Fact]
    public void NerissaHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(NerissaConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(NerissaConstants.Title);
    }
    
    [Fact]
    public void NerissaHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(NerissaConstants.Overflow);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(NerissaConstants.Drown);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(NerissaConstants.AncientSpirits);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(NerissaConstants.MesmerizingWater);
    }
}
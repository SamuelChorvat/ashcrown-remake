using Ashcrown.Remake.Core.Champions.Nazuc.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nazuc.Champion;

public class NazucTests
{
    [Fact]
    public void NazucHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(NazucConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(NazucConstants.Title);
    }
    
    [Fact]
    public void NazucHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(NazucConstants.SpearThrow);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(NazucConstants.SpearBarrage);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(NazucConstants.HuntersMark);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(NazucConstants.SeasonedStalker);
    }
}
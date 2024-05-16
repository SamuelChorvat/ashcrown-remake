using Ashcrown.Remake.Core.Champions.Izrin.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Izrin.Champion;

public class IzrinTests
{
    [Fact]
    public void IzrinHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(IzrinConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(IzrinConstants.Title);
    }
    
    [Fact]
    public void IzrinHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(IzrinConstants.Rake);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(IzrinConstants.BloodyStrike);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(IzrinConstants.Bite);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(IzrinConstants.WillOfTheUndead);
    }
}
using Ashcrown.Remake.Core.Champions.Evanore.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Evanore.Champion;

public class EvanoreTests
{
    [Fact]
    public void EvanoreHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EvanoreConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(EvanoreConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(EvanoreConstants.Title);
    }
    
    [Fact]
    public void EvanoreHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EvanoreConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(EvanoreConstants.MonstrousBear);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(EvanoreConstants.MurderOfCrows);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(EvanoreConstants.UmbraWolf);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(EvanoreConstants.HoundDefense);
    }
}
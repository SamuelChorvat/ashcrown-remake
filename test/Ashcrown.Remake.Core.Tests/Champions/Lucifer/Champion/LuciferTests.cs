using Ashcrown.Remake.Core.Champions.Lucifer.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lucifer.Champion;

public class LuciferTests
{
    [Fact]
    public void LuciferHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(LuciferConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(LuciferConstants.Title);
    }
    
    [Fact]
    public void LuciferHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(LuciferConstants.HeartOfDarkness);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(LuciferConstants.ShadowBolts);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(LuciferConstants.DarkChalice);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[2][1].Name.Should().Be(LuciferConstants.CursedCrow);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(LuciferConstants.DemonForm);
    }
}
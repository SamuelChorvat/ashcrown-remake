using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Arabela.Champion;

public class ArabelaTests
{
    [Fact]
    public void ArabelaHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(ArabelaConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(ArabelaConstants.Title);
    }
    
    [Fact]
    public void ArabelaHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(ArabelaConstants.DivineTrap);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(ArabelaConstants.FlashHeal);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(ArabelaConstants.PrayerOfHealing);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(ArabelaConstants.HolyShield);
    }
}
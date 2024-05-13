using Ashcrown.Remake.Core.Champions.Gruber.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gruber.Champion;

public class GruberTests
{
    [Fact]
    public void GruberHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(GruberConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(GruberConstants.Title);
    }
    
    [Fact]
    public void GruberHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(GruberConstants.PoisonInjection);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(GruberConstants.AdaptiveVirus);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(GruberConstants.ExplosiveLeech);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(GruberConstants.DNAEnhancement);
    }
}
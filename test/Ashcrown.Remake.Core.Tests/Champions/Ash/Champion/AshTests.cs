using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Ash.Champion;

public class AshTests
{
    [Fact]
    public void AshHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(AshConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(AshConstants.Title);
    }
    
    [Fact]
    public void AshHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(AshConstants.FireShield);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(AshConstants.Fireball);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[1][1].Name.Should().Be(AshConstants.FireWhirl);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[1][2].Name.Should().Be(AshConstants.Inferno);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(AshConstants.PhoenixFlames);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(AshConstants.FireBlock);
    }
}
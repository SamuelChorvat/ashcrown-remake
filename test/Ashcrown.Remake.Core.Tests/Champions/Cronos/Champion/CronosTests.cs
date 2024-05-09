using Ashcrown.Remake.Core.Champions.Cronos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cronos.Champion;

public class CronosTests
{
    [Fact]
    public void CronosHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(CronosConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(CronosConstants.Title);
    }
    
    [Fact]
    public void CronosHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(CronosConstants.GravityWell);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(CronosConstants.BattlefieldBulwark);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(CronosConstants.EMPBurst);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[2][1].Name.Should().Be(CronosConstants.PulseCannon);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(CronosConstants.MagitechCircuitry);
    }
}
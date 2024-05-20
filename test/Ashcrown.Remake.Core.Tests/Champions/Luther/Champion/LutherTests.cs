using Ashcrown.Remake.Core.Champions.Luther.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Luther.Champion;

public class LutherTests
{
    [Fact]
    public void LutherHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(LutherConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(LutherConstants.Title);
    }
    
    [Fact]
    public void LutherHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(LutherConstants.Flamestrike);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(LutherConstants.FieryBrand);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(LutherConstants.LivingForge);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[2][1].Name.Should().Be(LutherConstants.ForgeSpirit);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(LutherConstants.MoltenArmor);
    }
}
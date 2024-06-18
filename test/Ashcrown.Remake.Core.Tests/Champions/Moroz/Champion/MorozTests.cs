using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Moroz.Champion;

public class MorozTests
{
    [Fact]
    public void MorozHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(MorozConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(MorozConstants.Title);
    }
    
    [Fact]
    public void MorozHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(MorozConstants.Freeze);
        battleLogic.GetBattlePlayer(1).Champions[0].Abilities[0][1].Name.Should().Be(MorozConstants.Shatter);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(MorozConstants.IceBarrier);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(MorozConstants.FrozenArmor);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(MorozConstants.IceBlock);
    }
}
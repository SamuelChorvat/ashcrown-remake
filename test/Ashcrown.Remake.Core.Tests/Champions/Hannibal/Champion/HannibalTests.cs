using Ashcrown.Remake.Core.Champions.Hannibal.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hannibal.Champion;

public class HannibalTests
{
    [Fact]
    public void HannibalHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(HannibalConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(HannibalConstants.Title);
    }
    
    [Fact]
    public void HannibalHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(HannibalConstants.TasteForBlood);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(HannibalConstants.HealthFunnel);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(HannibalConstants.SacrificialPact);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(HannibalConstants.DemonicSkin);
    }
}
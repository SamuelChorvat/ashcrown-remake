using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Althalos.Champion;

public class AlthalosTests
{
    [Fact]
    public void AlthalosHasCorrectName()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Name.Should().Be(AlthalosConstants.Althalos);
        battleLogic.GetBattlePlayer(1).Champions[0].Title.Should().Be(AlthalosConstants.Title);
    }
    
    [Fact]
    public void AlthalosHasCorrectAbilities()
    {
        // Arrange & Act
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[0].Name.Should().Be(AlthalosConstants.HammerOfJustice);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[1].Name.Should().Be(AlthalosConstants.HolyLight);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[2].Name.Should().Be(AlthalosConstants.CrusaderOfLight);
        battleLogic.GetBattlePlayer(1).Champions[0].CurrentAbilities[3].Name.Should().Be(AlthalosConstants.DivineShield);
    }
}
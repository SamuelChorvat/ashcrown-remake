using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Althalos.Abilities;

public class HolyLightTests
{
    [Fact]
    public void HolyLightShouldHealTheCorrectAmount()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Name);
        var useHolyLight = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,2, 
            [1,0,0,0,0,0], [1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 50;

        // Act
        battleLogic.AbilitiesUsed(1, useHolyLight, useHolyLight.SpentEnergy!).Should().BeTrue();
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(75);
    }
}
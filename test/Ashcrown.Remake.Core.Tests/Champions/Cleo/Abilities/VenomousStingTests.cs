using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cleo.Abilities;

public class VenomousStingTests
{
    [Fact]
    public void VenomousStingDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useVenomousSting = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useVenomousSting, useVenomousSting.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CleoConstants.VenomousStingActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(1);
        ae.Stun.Should().BeTrue();
        ae.StunType.Should().BeEquivalentTo([AbilityClass.Physical, AbilityClass.Strategic]);
    }

}
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Dura.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Dura.Abilities;

public class WhirlwindTests
{
    [Fact]
    public void WhirlwindDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DuraConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();

        // Assert
        var champion1 = battleLogic.GetBattlePlayer(1).Champions[0];
        var champion2Player = battleLogic.GetBattlePlayer(2);

        champion2Player.Champions[0].Health.Should().Be(85);
        champion2Player.Champions[1].Health.Should().Be(85);
        champion2Player.Champions[2].Health.Should().Be(85);

        var whirlwindAe = champion1.ActiveEffectController.GetActiveEffectByName(DuraConstants.WhirlwindActiveEffect);
        whirlwindAe.Should().NotBeNull();
        whirlwindAe!.Invulnerability.Should().BeTrue();
        whirlwindAe.TimeLeft.Should().Be(1);
        whirlwindAe.TypeOfInvulnerability![0].Should().Be(AbilityClass.All);
    }
}
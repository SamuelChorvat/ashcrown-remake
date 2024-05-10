using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Dex.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Dex.Abilities;

public class NightbladeTests
{
    [Fact]
    public void NightbladeAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DexConstants.Name);
        var useNightblade = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useNightblade, useNightblade.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(DexConstants.NightbladeActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(3);
        ae.DestructibleDefense.Should().Be(30);
        ae.Invulnerability.Should().BeTrue();
        ae.TypeOfInvulnerability.Should().BeEquivalentTo(new[] {AbilityClass.Physical, AbilityClass.Strategic});
    }
}
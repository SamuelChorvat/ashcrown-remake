using Ashcrown.Remake.Core.Champions.Izrin.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Izrin.Abilities;

public class RakeTests
{
    [Fact]
    public void RakeDealsCorrectDamageAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(IzrinConstants.RakeActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(2);
        ae.AllDamageDealModifier.Points.Should().Be(-15);
    }

    [Fact]
    public void RakeDealsCorrectDamageAndAppliesAeWhenAllyIsDead()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 0;
        battleLogic.GetBattlePlayer(1).Champions[1].Alive = false;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(75);
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(IzrinConstants.RakeActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(3);
        ae.AllDamageDealModifier.Points.Should().Be(-15);
    }
}
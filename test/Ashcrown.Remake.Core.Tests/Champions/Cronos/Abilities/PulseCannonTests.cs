using Ashcrown.Remake.Core.Champions.Cronos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cronos.Abilities;

public class PulseCannonTests
{
    [Fact]
    public void PulseCannonDealsCorrectDamageAppliesActiveEffectAndSwapsAbilityToPulseCannons()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        var useEmpBurst = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 1, 1, 1]);
        var usedPulseCannons = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [1, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useEmpBurst, useEmpBurst.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, usedPulseCannons, usedPulseCannons.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(CronosConstants.PulseCannonMeActiveEffect).Should().BeTrue();

        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            champ.Health.Should().Be(55);
            var ae = champ.ActiveEffectController
                .GetActiveEffectByName(CronosConstants.PulseCannonTargetActiveEffect);
            ae.Should().NotBeNull();
            ae!.TimeLeft.Should().Be(1);
            ae.Damage1.Should().Be(15);
        }
    }
}
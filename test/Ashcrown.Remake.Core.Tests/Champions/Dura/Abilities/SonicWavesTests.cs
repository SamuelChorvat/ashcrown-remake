using Ashcrown.Remake.Core.Champions.Dura.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Dura.Abilities;

public class SonicWavesTests
{
    [Fact]
    public void SonicWavesDealCorrectDamageAndAppliesActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DuraConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion1 = battleLogic.GetBattlePlayer(1).Champions[0];
        var champion2 = battleLogic.GetBattlePlayer(2).Champions[0];

        champion1.ActiveEffectController.ActiveEffectPresentByActiveEffectName(DuraConstants.SonicWavesMeActiveEffect).Should().BeTrue();
        var targetAe = champion2.ActiveEffectController.GetActiveEffectByName(DuraConstants.SonicWavesTargetActiveEffect);
        targetAe.Should().NotBeNull();
        targetAe!.TimeLeft.Should().Be(1);
        targetAe.AllDamageDealModifier.Points.Should().Be(-5);
        champion2.Health.Should().Be(75);
    }

}
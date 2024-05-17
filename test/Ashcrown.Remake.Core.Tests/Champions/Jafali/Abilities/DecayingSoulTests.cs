using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jafali.Abilities;

public class DecayingSoulTests
{
    [Fact]
    public void DecayingSoulAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        var useAnger = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,1,0], [0,1,0,0]);
        var useDecayingSoul = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,0,1,0]);

        // Act
        var angerResult = battleLogic.AbilitiesUsed(1, useAnger, useAnger.SpentEnergy!);
        angerResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var decayingSoulResult = battleLogic.AbilitiesUsed(1, useDecayingSoul, useDecayingSoul.SpentEnergy!);
        decayingSoulResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(95);
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.DecayingSoulActiveEffect).Should().BeTrue();
        var decayingSoulAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(JafaliConstants.DecayingSoulActiveEffect);
        decayingSoulAe!.TimeLeft.Should().Be(4);
        decayingSoulAe.AfflictionDamage.Should().BeTrue();
    }
}
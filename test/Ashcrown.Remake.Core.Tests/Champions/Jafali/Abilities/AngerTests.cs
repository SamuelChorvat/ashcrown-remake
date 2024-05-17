using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jafali.Abilities;

public class AngerTests
{
    [Fact]
    public void AngerAppliesCorrectAesAndIsReplacedByDecayingSoul()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        var useAnger = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useAnger, useAnger.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.AngerActiveEffect).Should().BeTrue();
        var angerAe = champion.ActiveEffectController.GetActiveEffectByName(JafaliConstants.AngerActiveEffect);
        angerAe!.Infinite.Should().BeTrue();
        angerAe.DestructibleDefense.Should().Be(15);
        champion.AbilityController.GetCurrentAbility(1).Name.Should().Be("Decaying Soul");

        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(95);
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.DecayingSoulActiveEffect).Should().BeTrue();
        var decayingSoulAe = enemyChampion.ActiveEffectController.GetActiveEffectByName(JafaliConstants.DecayingSoulActiveEffect);
        decayingSoulAe!.TimeLeft.Should().Be(4);
    }

    [Fact]
    public void AngerAppliesDecayingSoulWhenDestructibleIsDestroyedAndReplacesDecayingSoul()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(JafaliConstants.Name, 
            BranleyConstants.Name);
        var useAnger = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 1,
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useAnger, useAnger.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.AngerActiveEffect).Should().BeFalse();
        champion.AbilityController.GetCurrentAbility(1).Name.Should().Be("Anger");

        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[1];
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.DecayingSoulActiveEffect).Should().BeTrue();
        enemyChampion.ActiveEffectController.GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect).Should().Be(1);
        var decayingSoulAe = enemyChampion.ActiveEffectController.GetActiveEffectByName(JafaliConstants.DecayingSoulActiveEffect);
        decayingSoulAe!.TimeLeft.Should().Be(5);
    }
}
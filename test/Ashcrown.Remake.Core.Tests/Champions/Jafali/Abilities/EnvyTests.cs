using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jafali.Abilities;

public class EnvyTests
{
    [Fact]
    public void EnvyAppliesCorrectAesAndIsReplacedByAvarice()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        var useEnvy = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [0,2,0,0]);

        // Act
        var envyResult = battleLogic.AbilitiesUsed(1, useEnvy, useEnvy.SpentEnergy!);
        envyResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.EnvyActiveEffect).Should().BeTrue();
        var envyAe = champion.ActiveEffectController.GetActiveEffectByName(JafaliConstants.EnvyActiveEffect);
        envyAe!.Infinite.Should().BeTrue();
        envyAe.DestructibleDefense.Should().Be(30);
        champion.AbilityController.GetCurrentAbility(2).Name.Should().Be("Avarice");

        for (int i = 0; i < 3; i++)
        {
            var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[i];
            enemyChampion.Health.Should().Be(95);
            enemyChampion.ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(JafaliConstants.DecayingSoulActiveEffect).Should().BeTrue();
            var decayingSoulAe = enemyChampion.ActiveEffectController
                .GetActiveEffectByName(JafaliConstants.DecayingSoulActiveEffect);
            decayingSoulAe!.TimeLeft.Should().Be(4);
        }
    }

    [Fact]
    public void EnvyAppliesDecayingSoulWhenDestructibleIsDestroyedAndReplacesAvarice()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(JafaliConstants.Name, 
            BranleyConstants.Name);
        var useEnvy = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [0,2,0,0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 1,
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useEnvy, useEnvy.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JafaliConstants.EnvyActiveEffect).Should().BeFalse();
        champion.AbilityController.GetCurrentAbility(2).Name.Should().Be("Envy");

        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[1];
        enemyChampion.ActiveEffectController
            .GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect).Should().Be(2);
    }
}
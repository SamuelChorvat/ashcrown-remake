using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Hrom.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hrom.Abilities;

public class LightningBarrierTests
{
    [Fact]
    public void LightningBarrierAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HromConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,1,0,0,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var activeEffect = battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .GetActiveEffectByName(HromConstants.LightningBarrierActiveEffect);
        activeEffect.Should().NotBeNull();
        activeEffect!.Hidden.Should().BeTrue();
        activeEffect.TimeLeft.Should().Be(1);
    }

    [Fact]
    public void LightningBarrierCorrectlyCountersAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(HromConstants.Name, 
            BranleyConstants.Name);
        var useLightningBarrier = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,1,0,0,0,0], [1,0,0,0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,0,1,0], [0,1,1,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useLightningBarrier, useLightningBarrier.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HromConstants.LightningBarrierEndActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HromConstants.LightningBarrierCounterActiveEffect).Should().BeTrue();
    }
}
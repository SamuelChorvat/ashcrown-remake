using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Sarfu.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cedric.Abilities;

public class TimeWarpTests
{
    [Fact]
    public void TimeWarpAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useTimeWarp = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [0, 0, 0, 2]);

        // Act
        battleLogic.AbilitiesUsed(1, useTimeWarp, useTimeWarp.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        foreach (var champion in battleLogic.GetBattlePlayer(1).Champions)
        {
            var ae = champion.ActiveEffectController.GetActiveEffectByName(CedricConstants.TimeWarpActiveEffect);
            ae.Should().NotBeNull();
            ae!.TimeLeft.Should().Be(2);
        }
    }
    
    [Fact]
    public void TimeWarpCorrectlyReducesCooldownAndCost()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithThreeDifferentChampions(
            CedricConstants.Name,
            AkioConstants.Name,
            AnielConstants.Name,
            BranleyConstants.Name,
            SarfuConstants.Name,
            EluardConstants.Name);
        var useTimeWarp = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [0, 0, 0, 2]);

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCost()[4].Should().Be(1);
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCooldown().Should().Be(4);
        battleLogic.AbilitiesUsed(1, useTimeWarp, useTimeWarp.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var updatedCost = battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCost()[4];
        var updatedCd = battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCooldown();
        updatedCost.Should().Be(0);
        updatedCd.Should().Be(3);
    }
    
    [Fact]
    public void TimeWarpCorrectlyRemovesCostAndCooldownReduction()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useTimeWarp = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [0, 0, 0, 2]);

        // Act
        battleLogic.AbilitiesUsed(1, useTimeWarp, [0, 0, 0, 2]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCost()[4].Should().Be(0);
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCooldown().Should().Be(3);
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 4);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCost()[4].Should().Be(1);
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(4).GetCurrentCooldown().Should().Be(4);
    }
}
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;
using AshConstants = Ashcrown.Remake.Core.Champions.Ash.Champion.AshConstants;

namespace Ashcrown.Remake.Core.Tests.Champions.Ash.Abilities;

public class InfernoTests
{
    [Fact]
    public void InfernoDoesCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useInferno = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);

        // Act
        for (var i = 0; i < 8; i++)  // Repeatedly use Phoenix Flames to build stacks as implied by the original test
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, [0, 0, 0, 0]).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect)!.Stacks.Should().Be(8);
        battleLogic.AbilitiesUsed(1, useInferno, [1, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be(AshConstants.Inferno);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(0);
        battleLogic.GetBattlePlayer(2).Champions[0].Alive.Should().BeFalse();
    }
}

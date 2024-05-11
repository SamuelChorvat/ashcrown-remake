using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Ash.Abilities;

public class FireWhirlTests
{
    [Fact]
    public void FireWhirlDoesCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useFireWhirl = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);

        // Act
        for (int i = 0; i < 5; i++)
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }
        battleLogic.AbilitiesUsed(1, useFireWhirl, useFireWhirl.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be(AshConstants.FireWhirl);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(15);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireWhirlActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireWhirlActiveEffect)!.Stun.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireWhirlActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireWhirlActiveEffect)!.StunType![0].Should().Be(AbilityClass.All);
    }
    
    [Fact]
    public void FireWhirlShouldStun()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useFireWhirl = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);
        var useFireball = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);

        // Act
        for (var i = 0; i < 5; i++)
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }
        battleLogic.AbilitiesUsed(1, useFireWhirl, useFireWhirl.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var act = () => battleLogic.AbilitiesUsed(2, useFireball, useFireball.SpentEnergy!);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Stunned to use problem");
    }
}

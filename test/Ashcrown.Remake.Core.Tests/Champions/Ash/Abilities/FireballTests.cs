using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Ash.Abilities;

public class FireballTests
{
    [Fact]
    public void FireballShouldDealCorrectDamageAndApplyActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var useFireball = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);
    
        // Act
        battleLogic.AbilitiesUsed(1, useFireball, useFireball.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
    
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(65);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireballActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireballActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.FireballActiveEffect)!.AllDamageDealModifier.Points.Should().Be(-20);
    }
    
    [Fact]
    public void FireballShouldNotBeUsableAt5StacksOfPhoenixFlames()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useFireball = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 1]);

        // Act and Assert
        for (var i = 0; i < 5; i++)
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }
        battleLogic.AbilitiesUsed(1, useFireball, useFireball.SpentEnergy!).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireballActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AshConstants.FireWhirlActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be("Fire Whirl");
    }
}
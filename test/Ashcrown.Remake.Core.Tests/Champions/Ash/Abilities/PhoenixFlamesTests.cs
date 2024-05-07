using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Ash.Abilities;

public class PhoenixFlamesTests
{
    [Fact]
    public void PhoenixFlamesCorrectlyAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.Stacks.Should().Be(1);
        ae.AllDamageReceiveModifier.Points.Should().Be(-5);
        ae.AllDamageDealModifier.Points.Should().Be(5);
    }
    
    [Fact]
    public void PhoenixFlamesCorrectlyStacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act
        for (int i = 0; i < 3; i++)
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AshConstants.PhoenixFlamesActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.Stacks.Should().Be(3);
        ae.AllDamageReceiveModifier.Points.Should().Be(-15);
        ae.AllDamageDealModifier.Points.Should().Be(15);
    }
    
    [Fact]
    public void PhoenixFlamesSwapsFireballToFireWhirlAt5Stacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act and Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be(AshConstants.Fireball);
        for (int i = 0; i < 5; i++)
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be(AshConstants.FireWhirl);
    }

    [Fact]
    public void PhoenixFlamesSwapsFireWhirlToInfernoAt8Stacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AshConstants.Name);
        var usePhoenixFlames = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [1, 0, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act and Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be("Fireball");
        for (int i = 0; i < 8; i++)
        {
            battleLogic.AbilitiesUsed(1, usePhoenixFlames, usePhoenixFlames.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        }

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController
            .GetCurrentAbility(2).Name.Should().Be(AshConstants.Inferno);
    }
}
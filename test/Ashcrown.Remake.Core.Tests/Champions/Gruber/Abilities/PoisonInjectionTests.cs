using Ashcrown.Remake.Core.Champions.Fae.Champion;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gruber.Abilities;

public class PoisonInjectionTests
{
    [Fact]
    public void PoisonInjectionDealsCorrectDamageAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        champion.Health.Should().Be(90);
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartOneActiveEffect).Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionPartOneActiveEffect)!.TimeLeft.Should().Be(1);
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionPartOneActiveEffect)!.Stun.Should().BeFalse();
    }

    [Fact]
    public void PoisonInjectionPartTwoWorks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 3);

        // Assert
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        champion.Health.Should().Be(70);
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartOneActiveEffect).Should().BeFalse();
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartTwoActiveEffect).Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionPartTwoActiveEffect)!.TimeLeft.Should().Be(1);
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionPartTwoActiveEffect)!.Stun.Should().BeTrue();
    }

    [Fact]
    public void PoisonInjectionLeavesStacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 5);

        // Assert
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        champion.Health.Should().Be(70);
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionPartTwoActiveEffect).Should().BeFalse();
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.PoisonInjectionStacksActiveEffect).Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionStacksActiveEffect)!.Infinite.Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionStacksActiveEffect)!.Stackable.Should().BeTrue();
    }

    [Fact]
    public void PoisonInjectionStacksIncreaseStunDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);
        battleLogic.AbilitiesUsed(1, usedAbilities, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 3);

        // Assert
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        champion.Health.Should().Be(40);
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.PoisonInjectionPartTwoActiveEffect)!.TimeLeft.Should().Be(2);
    }

    [Fact]
    public void PoisonInjectionShouldNotTriggerAbilityCheckIfRemovedOnYourTurn()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(GruberConstants.Name, FaeConstants.Name);
        var usedAbility = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        var enemyAbilities = BattleTestHelpers.CreateEndTurnWithTwoAbilitiesUsed(2, 3,
            [1, 0, 0, 0, 0, 0], 1, 1, [0, 0, 0, 1, 0, 0], [2, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbility, usedAbility.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.AbilitiesUsed(2, enemyAbilities, enemyAbilities.SpentEnergy!).Should().BeTrue();
    }
}
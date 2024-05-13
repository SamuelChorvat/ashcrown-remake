using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Gruber.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gruber.Abilities;

public class ExplosiveLeechTests
{
    [Fact]
    public void ExplosiveLeechAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[1];
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.ExplosiveLeechActiveEffect).Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.ExplosiveLeechActiveEffect)!.Infinite.Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.ExplosiveLeechActiveEffect)!.Hidden.Should().BeTrue();
        champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.ExplosiveLeechActiveEffect)!.Stackable.Should().BeTrue();
    }

    [Fact]
    public void ExplosiveLeechCorrectlyExplodes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(GruberConstants.Name, 
            AlthalosConstants.Name);
        var useExplosiveLeech = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 0, 1, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useExplosiveLeech, [0, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, [0, 0, 1, 0]).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(70);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GruberConstants.ExplosiveLeechEndActiveEffect).Should().BeTrue();
    }

    [Fact]
    public void ExplosiveLeechCorrectlyStacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(GruberConstants.Name, 
            AlthalosConstants.Name);
        var useExplosiveLeech = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 1, 0, 0, 0, 0], [0, 0, 0, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 0, 1, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useExplosiveLeech, [0, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useExplosiveLeech, [0, 0, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, [0, 0, 1, 0]).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(1).Champions[1].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(1).Champions[1].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GruberConstants.ExplosiveLeechEndActiveEffect).Should().BeTrue();
    }
}
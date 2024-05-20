using Ashcrown.Remake.Core.Champions.Luther.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Luther.Abilities;

public class FieryBrandTests
{
    [Fact]
    public void FieryBrandAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useFieryBrand = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useFieryBrand, useFieryBrand.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.FieryBrandTargetActiveEffect).Should().BeTrue();
        var fieryBrandAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.FieryBrandTargetActiveEffect);
        fieryBrandAe!.TimeLeft.Should().Be(1);
        fieryBrandAe.Hidden.Should().BeTrue();
    }

    [Fact]
    public void FieryBrandCorrectlyCounterStrategic()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useFieryBrand = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1,0,0,0,0,0], [1,0,0,0]);

        // Act
        var fieryBrandResult = battleLogic.AbilitiesUsed(1, useFieryBrand, useFieryBrand.SpentEnergy!);
        fieryBrandResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var invulnerabilityResult = battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!);
        invulnerabilityResult.Should().BeTrue();

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.FieryBrandCounterActiveEffect).Should().BeTrue();
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.MoltenArmorActiveEffect).Should().BeFalse();
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.FieryBrandEndActiveEffect).Should().BeTrue();
    }

    [Fact]
    public void FieryBrandIncreasesFlamestrikeDurationAfterSuccessfulCounter()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useFieryBrand = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1,0,0,0,0,0], [1,0,0,0]);
        var useFlameStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [1,1,0,0]);

        // Act
        var fieryBrandResult = battleLogic.AbilitiesUsed(1, useFieryBrand, useFieryBrand.SpentEnergy!);
        fieryBrandResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var invulnerabilityResult = battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!);
        invulnerabilityResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        var flameStrikeResult = battleLogic.AbilitiesUsed(1, useFlameStrike, useFlameStrike.SpentEnergy!);
        flameStrikeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        var flameStrikeAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.FlamestrikeTargetActiveEffect);
        flameStrikeAe!.TimeLeft.Should().Be(3);
    }
}
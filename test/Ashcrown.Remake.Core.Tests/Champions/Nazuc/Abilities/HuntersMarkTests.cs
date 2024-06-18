using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Nazuc.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nazuc.Abilities;

public class HuntersMarkTests
{
    [Fact]
    public void HuntersMarkAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        var useHuntersMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [0,0,0,1,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useHuntersMark, useHuntersMark.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(NazucConstants.HuntersMarkActiveEffect).Should().BeTrue();
        var ae = enemyChampion.ActiveEffectController.GetActiveEffectByName(NazucConstants.HuntersMarkActiveEffect);
        ae!.DisableInvulnerability.Should().BeTrue();
        ae.DisableDamageReceiveReduction.Should().BeTrue();
        ae.TimeLeft.Should().Be(3);
    }

    [Fact]
    public void HuntersMarkCorrectlyDisablesInvulnerability()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NazucConstants.Name);
        var useHuntersMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [0,0,0,1,0,0], [0,1,0,0]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4, 
            [1,0,0,0,0,0], [0,1,0,0]);
        var useSpearThrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        var resultHuntersMark = battleLogic.AbilitiesUsed(1, useHuntersMark, useHuntersMark.SpentEnergy!);
        resultHuntersMark.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var resultInvulnerability = battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!);
        resultInvulnerability.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        var resultSpearThrow = battleLogic.AbilitiesUsed(1, useSpearThrow, useSpearThrow.SpentEnergy!);
        resultSpearThrow.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(65);
    }

    [Fact]
    public void HuntersMarkCorrectlyDisablesDamageReceiveReduction()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(NazucConstants.Name, 
            EluardConstants.Name);
        var useHuntersMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [0,0,0,1,0,0], [0,1,0,0]);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1,0,0,0,0,0], [0,1,0,0]);
        var useSpearThrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [0,1,1,0]);

        // Act
        var resultHuntersMark = battleLogic.AbilitiesUsed(1, useHuntersMark, useHuntersMark.SpentEnergy!);
        resultHuntersMark.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var resultUnyieldingWill = battleLogic.AbilitiesUsed(2, useUnyieldingWill, useUnyieldingWill.SpentEnergy!);
        resultUnyieldingWill.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        var resultSpearThrow = battleLogic.AbilitiesUsed(1, useSpearThrow, useSpearThrow.SpentEnergy!);
        resultSpearThrow.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(65);
    }
}
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Champions.Ash.Champion;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Branley.Abilities;

public class RaiseTheFlagTests
{
    [Fact]
    public void RaiseTheFlagAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        var useRaiseTheFlag = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useRaiseTheFlag, useRaiseTheFlag.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var aeMe = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BranleyConstants.RaiseTheFlagMeActiveEffect);
        aeMe.Should().NotBeNull();
        aeMe!.TimeLeft.Should().Be(3);
        aeMe.AllDamageReceiveModifier.Points.Should().Be(-5);

        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            var aeTarget = champ.ActiveEffectController
                .GetActiveEffectByName(BranleyConstants.RaiseTheFlagTargetActiveEffect);
            aeTarget.Should().NotBeNull();
            aeTarget!.TimeLeft.Should().Be(2);
        }
    }
    
    [Fact]
    public void RaiseTheFlagIgnoresInvulnerability()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);
        var useRaiseTheFlag = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.AbilitiesUsed(1, useRaiseTheFlag, useRaiseTheFlag.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var aeTarget = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(BranleyConstants.RaiseTheFlagTargetActiveEffect);
        aeTarget.Should().NotBeNull();
        aeTarget!.TimeLeft.Should().Be(2);
    }
    
    [Fact]
    public void RaiseTheFlagIncreasesCostOfPhysicalAndStrategicAbilitiesOnly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithThreeDifferentChampions(
            BranleyConstants.Name, BranleyConstants.Name, BranleyConstants.Name,
            AlthalosConstants.Name, AshConstants.Name, AnielConstants.Name);
        var useRaiseTheFlag = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useRaiseTheFlag, useRaiseTheFlag.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            foreach (var abilities in champ.Abilities)
            {
                foreach (var ability in abilities)
                {
                    if (ability.AbilityClassesContains(AbilityClass.Physical) 
                        || ability.AbilityClassesContains(AbilityClass.Strategic))
                    {
                        ability.GetCurrentCost()[4].Should().Be(ability.OriginalCost[4] + 1);
                    }
                }
            }
        }
    }
    
    [Fact]
    public void RaiseTheFlagCostIncreaseIsRemovedCorrectly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithThreeDifferentChampions(
            BranleyConstants.Name, BranleyConstants.Name, BranleyConstants.Name,
            AlthalosConstants.Name, AshConstants.Name, AnielConstants.Name);
        var useRaiseTheFlag = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useRaiseTheFlag, useRaiseTheFlag.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 4);

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            foreach (var abilities in champ.Abilities)
            {
                foreach (var ability in abilities)
                {
                    if (ability.AbilityClassesContains(AbilityClass.Physical) 
                        || ability.AbilityClassesContains(AbilityClass.Strategic))
                    {
                        ability.GetCurrentCost()[4].Should().Be(ability.OriginalCost[4]);
                    }
                }
            }
        }
    }
}
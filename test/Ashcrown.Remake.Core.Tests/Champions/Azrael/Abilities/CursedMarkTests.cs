using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Azrael.Abilities;

public class CursedMarkTests
{
    [Fact]
    public void CursedMarkAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useCursedMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useCursedMark, useCursedMark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.CursedMarkMeActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(1);
        ae.Invulnerability.Should().BeTrue();
        ae.TypeOfInvulnerability![0].Should().Be(AbilityClass.Strategic);
    }
    
    [Fact]
    public void CursedMarkWillApplyOnEnemyTargetingAzraelWithAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(AzraelConstants.Name, AlthalosConstants.Name);
        var useCursedMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useCursedMark, useCursedMark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 2);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(95);
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.CursedMarkTargetActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.Stacks.Should().Be(1);
    }
    
    [Fact]
    public void CursedMarkStacksCorrectly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(AzraelConstants.Name, 
            AlthalosConstants.Name);
        var useCursedMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act and Assert
        battleLogic.AbilitiesUsed(1, useCursedMark, useCursedMark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useCursedMark, useCursedMark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 2);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.CursedMarkTargetActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.Stacks.Should().Be(2);
    }
}
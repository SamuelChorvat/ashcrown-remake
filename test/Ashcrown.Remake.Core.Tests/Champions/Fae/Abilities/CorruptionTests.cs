using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Dex.Champion;
using Ashcrown.Remake.Core.Champions.Fae.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Fae.Abilities;

public class CorruptionTests
{
    [Fact]
    public void CorruptionAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(FaeConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        var corruptionAe = champion.ActiveEffectController.GetActiveEffectByName(FaeConstants.CorruptionActiveEffect);
    
        champion.Health.Should().Be(90);
        corruptionAe.Should().NotBeNull();
        corruptionAe!.TimeLeft.Should().Be(2);
        corruptionAe.AfflictionDamage.Should().BeTrue();
    }
    
    [Fact]
    public void CorruptionCannotBeUsedOnTargetAlreadyAffectedByIt()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(FaeConstants.Name);
        var useCorruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useCorruption, useCorruption.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 4);
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        var corruptionAe = champion.ActiveEffectController.GetActiveEffectByName(FaeConstants.CorruptionActiveEffect);
        champion.Health.Should().Be(80);
        corruptionAe!.Should().NotBeNull();

        // Assert
        battleLogic.AbilitiesUsed(1, useCorruption, useCorruption.SpentEnergy!).Should().BeFalse();
    }
    
    [Fact]
    public void CorruptionDealsAfflictionDamageReduction()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(FaeConstants.Name, 
            AlthalosConstants.Name);
        var useCrusaderOfLight = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);
        var useCorruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useCrusaderOfLight, useCrusaderOfLight.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.AbilitiesUsed(1, useCorruption, useCorruption.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
    }

    [Fact]
    public void CorruptionDealsAfflictionDamageDestructible()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(
            FaeConstants.Name, DexConstants.Name);
        var useNightblade = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(3, 3,
            [0, 0, 1, 0, 0, 0], [0, 1, 0, 1]);
        var useCorruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 0, 0, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useNightblade, useNightblade.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.AbilitiesUsed(1, useCorruption, useCorruption.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(90);
    }
}
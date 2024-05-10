using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Dex.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Dex.Abilities;

public class GarroteTests
{
    [Fact]
    public void GarroteDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DexConstants.Name);
        var useGarrote = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 1, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useGarrote, useGarrote.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var target = battleLogic.GetBattlePlayer(2).Champions[0];
        target.Health.Should().Be(80);
        var ae = target.ActiveEffectController.GetActiveEffectByName(DexConstants.GarroteActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(1);
        ae.Stun.Should().BeTrue();
        ae.StunType.Should().BeEquivalentTo([AbilityClass.Physical,AbilityClass.Strategic]);
        battleLogic.GetBattlePlayer(2).Champions[1]
            .ActiveEffectController.GetActiveEffectByName(DexConstants.GarroteActiveEffect).Should().BeNull();
        battleLogic.GetBattlePlayer(2).Champions[2]
            .ActiveEffectController.GetActiveEffectByName(DexConstants.GarroteActiveEffect).Should().BeNull();
    }
    
    [Fact]
    public void GarroteTargetsAllEnemiesDuringNightbladeAndCostReduced()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DexConstants.Name);
        var useNightblade = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 1]);
        var useGarrote = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 1, 1], [0, 1, 0, 0]);

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(2).GetCurrentCost()[4].Should().Be(1);
        battleLogic.AbilitiesUsed(1, useNightblade, useNightblade.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useGarrote, useGarrote.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(2).GetCurrentCost()[4].Should().Be(0);

        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            champ.Health.Should().Be(80);
            var ae = champ.ActiveEffectController.GetActiveEffectByName(DexConstants.GarroteActiveEffect);
            ae.Should().NotBeNull();
            ae!.TimeLeft.Should().Be(1);
            ae.Stun.Should().BeTrue();
            ae.StunType.Should().BeEquivalentTo(new[] {AbilityClass.Physical, AbilityClass.Strategic});
        }
    }

}
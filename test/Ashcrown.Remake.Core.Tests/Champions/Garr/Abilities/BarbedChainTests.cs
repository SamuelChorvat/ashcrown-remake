using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Garr.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Garr.Abilities;

public class BarbedChainTests
{
    [Fact]
    public void BarbedChainDealsCorrectDamageAndAppliesAes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GarrConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 1, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GarrConstants.BarbedChainTargetActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(GarrConstants.BarbedChainTargetActiveEffect)!.TimeLeft.Should().Be(1);

        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GarrConstants.BarbedChainMeActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(GarrConstants.BarbedChainInvulnerabilityActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(GarrConstants.BarbedChainInvulnerabilityActiveEffect)!.Invulnerability.Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(GarrConstants.BarbedChainInvulnerabilityActiveEffect)!.TypeOfInvulnerability![0].Should().Be(AbilityClass.All);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(GarrConstants.BarbedChainInvulnerabilityActiveEffect)!.TimeLeft.Should().Be(1);
    }
    
    [Fact]
    public void BarbedChainDealsCorrectDamageWhileRecklessnessIsActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GarrConstants.Name);
        var useRecklessness = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);
        var useBarbedChain = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 1, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useRecklessness, useRecklessness.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useBarbedChain, useBarbedChain.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
    }
}
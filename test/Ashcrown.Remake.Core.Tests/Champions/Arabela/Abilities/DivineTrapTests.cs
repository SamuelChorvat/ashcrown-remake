using Ashcrown.Remake.Core.Champions.Arabela.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Arabela.Abilities;

public class DivineTrapTests
{
        [Fact]
    public void DivineTrapAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var usedDivineTrap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1,
            [0,0,0,1,0,0], [0,0,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedDivineTrap, usedDivineTrap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var playerInfo = battleLogic.GetBattlePlayer(1);
        playerInfo.Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapMeActiveEffect).Should().BeTrue();
        playerInfo.Champions[0].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.DivineTrapMeActiveEffect)!.TimeLeft.Should().Be(1);
        playerInfo.Champions[0].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.DivineTrapMeActiveEffect)!.AllDamageReceiveModifier.Points.Should().Be(-15);
        
        var targetInfo = battleLogic.GetBattlePlayer(2);
        targetInfo.Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapTargetActiveEffect).Should().BeTrue();
        targetInfo.Champions[0].ActiveEffectController
            .GetActiveEffectByName(ArabelaConstants.DivineTrapTargetActiveEffect)!.Hidden.Should().BeTrue();
    }

    [Fact]
    public void DivineTrapDoesNotDealBonusDamageIfTargetDoesNotUseAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var usedDivineTrap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1,
            [0,0,0,1,0,0], [0,0,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedDivineTrap, usedDivineTrap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapEndActiveEffect).Should().BeTrue();
    }

    [Fact]
    public void DivineTrapDealsBonusDamageIfTargetUsesAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(ArabelaConstants.Name);
        var useDivineTrap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1,
            [0,0,0,1,0,0], [0,0,0,1]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,4,
            [1,0,0,0,0,0], [0,0,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useDivineTrap, useDivineTrap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(ArabelaConstants.DivineTrapEndActiveEffect).Should().BeTrue();
    }
}
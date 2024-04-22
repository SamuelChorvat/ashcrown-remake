using Ashcrown.Remake.Core.Champions.Sarfu.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Sarfu.Abilities;

public class DuelTests
{
    [Fact]
    public void DuelAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(SarfuConstants.Sarfu);
        var useDuel = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useDuel, useDuel.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(SarfuConstants.DuelMeActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(SarfuConstants.DuelMeActiveEffect)!.TimeLeft.Should().Be(4);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(SarfuConstants.DuelMeActiveEffect)!.AllDamageReceiveModifier.Points.Should().Be(-10);
        
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(SarfuConstants.DuelTargetActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(SarfuConstants.DuelTargetActiveEffect)!.DisableDamageReceiveReduction.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(SarfuConstants.DuelTargetActiveEffect)!.DisableInvulnerability.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(SarfuConstants.DuelTargetActiveEffect)!.TimeLeft.Should().Be(3);
    }

    [Fact]
    public void DuelEndsIfSarfuDies()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(SarfuConstants.Sarfu);
        var useDuel = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useDuel, useDuel.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(SarfuConstants.DuelTargetActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ChampionController.OnDeath();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(SarfuConstants.DuelMeActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(SarfuConstants.DuelTargetActiveEffect).Should().BeFalse();
    }
}
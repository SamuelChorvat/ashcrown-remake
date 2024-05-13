using Ashcrown.Remake.Core.Champions.Gwen.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gwen.Abilities;

public class CharmTests
{
    [Fact]
    public void CharmAppliesCorrectAesAndDealsDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GwenConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 1, 1], [0, 1, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion0 = battleLogic.GetBattlePlayer(2).Champions[0];
        var champion1 = battleLogic.GetBattlePlayer(2).Champions[1];
        var champion2 = battleLogic.GetBattlePlayer(2).Champions[2];

        champion0.Health.Should().Be(85);
        champion0.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmTargetActiveEffect).Should().BeTrue();
        champion0.ActiveEffectController.GetActiveEffectByName(GwenConstants.CharmTargetActiveEffect)!.TimeLeft.Should().Be(1);

        champion1.Health.Should().Be(85);
        champion1.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmTargetActiveEffect).Should().BeTrue();
        champion1.ActiveEffectController.GetActiveEffectByName(GwenConstants.CharmTargetActiveEffect)!.TimeLeft.Should().Be(1);

        champion2.Health.Should().Be(85);
        champion2.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmTargetActiveEffect).Should().BeTrue();
        champion2.ActiveEffectController.GetActiveEffectByName(GwenConstants.CharmTargetActiveEffect)!.TimeLeft.Should().Be(1);

        var ownChampion = battleLogic.GetBattlePlayer(1).Champions[0];
        ownChampion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmMeActiveEffect).Should().BeTrue();
        ownChampion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmBuffActiveEffect).Should().BeTrue();
        ownChampion.ActiveEffectController.GetActiveEffectByName(GwenConstants.CharmBuffActiveEffect)!.TimeLeft.Should().Be(2);
        ownChampion.ActiveEffectController.GetActiveEffectByName(GwenConstants.CharmBuffActiveEffect)!.AllDamageReceiveModifier.Points.Should().Be(-15);
    }

    [Fact]
    public void CharmDealsCorrectDamageOverItsDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GwenConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 1, 1], [0, 1, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 3);

        // Assert
        var champion = battleLogic.GetBattlePlayer(2).Champions[0];
        champion.Health.Should().Be(70);
        champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GwenConstants.CharmTargetActiveEffect).Should().BeFalse();
    }
}
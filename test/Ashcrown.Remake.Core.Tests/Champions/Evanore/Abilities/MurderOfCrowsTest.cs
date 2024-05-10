using Ashcrown.Remake.Core.Champions.Evanore.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Evanore.Abilities;

public class MurderOfCrowsTest
{
    [Fact]
    public void MurderOfCrowsAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EvanoreConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion2 = battleLogic.GetBattlePlayer(2).Champions[0];
        var champion1 = battleLogic.GetBattlePlayer(1).Champions[0];

        champion2.Health.Should().Be(85);
        var murderOfCrowsTargetAe = champion2.ActiveEffectController
            .GetActiveEffectByName(EvanoreConstants.MurderOfCrowsTargetActiveEffect);
        murderOfCrowsTargetAe.Should().NotBeNull();
        murderOfCrowsTargetAe!.TimeLeft.Should().Be(2);

        champion1.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(EvanoreConstants.MurderOfCrowsMeActiveEffect).Should().BeTrue();
    }
    
    [Fact]
    public void MurderOfCrowsDealsCorrectDamageOverWholeDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EvanoreConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(55);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(EvanoreConstants.MurderOfCrowsTargetActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(EvanoreConstants.MurderOfCrowsMeActiveEffect).Should().BeFalse();
    }
}
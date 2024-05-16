using Ashcrown.Remake.Core.Champions.Hrom.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hrom.Abilities;

public class LightningStrikeTests
{
    [Fact]
    public void LightingStrikeDealsCorrectDamageAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HromConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(75);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HromConstants.LightningStrikeActiveEffect).Should().BeTrue();
    }

    [Fact]
    public void LightningStrikeCorrectlyAppliesHelperAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HromConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HromConstants.LightningStrikeActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HromConstants.LightningStrikeHelperActiveEffect).Should().BeTrue();
    }
}
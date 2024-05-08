using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using Ashcrown.Remake.Core.Champions.Cleo.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cleo.Abilities;

public class ChitinRegenerationTests
{
    [Fact]
    public void ChitinRegenerationFullyHeals()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CleoConstants.Name);
        var useChitinRegeneration = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [2, 0, 0, 0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 1;

        // Act
        battleLogic.AbilitiesUsed(1, useChitinRegeneration, useChitinRegeneration.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(100);
    }
    
    [Fact]
    public void ChitinRegenerationRemovesHarmful()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(CleoConstants.Name, 
            CedricConstants.Name);
        var useDarkSoul = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 1, [0, 0, 0, 1, 0, 0], 
            [0, 0, 0, 1]);
        var useChitinRegeneration = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [1, 0, 0, 0, 0, 0], [
            2, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useDarkSoul, useDarkSoul.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        var aePresentBefore = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(CedricConstants.DarkSoulTargetActiveEffect);
        aePresentBefore.Should().BeTrue();

        battleLogic.AbilitiesUsed(1, useChitinRegeneration, useChitinRegeneration.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var aePresentAfter = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(CedricConstants.DarkSoulTargetActiveEffect);
        aePresentAfter.Should().BeFalse();
    }
}
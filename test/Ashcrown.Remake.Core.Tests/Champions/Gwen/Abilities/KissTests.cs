using Ashcrown.Remake.Core.Champions.Gwen.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gwen.Abilities;

public class KissTests
{
    [Fact]
    public void KissCannotBeUsedWithoutCharmActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GwenConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        
        // Act
        var act = () => battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);
        
        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }

    [Fact]
    public void KissDealCorrectDamageAndRemovesEnergy()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GwenConstants.Name);
        var useCharm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 1, 1], [0, 1, 0, 1]);
        var useKiss = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useCharm, useCharm.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(43);
        battleLogic.AbilitiesUsed(1, useKiss, useKiss.SpentEnergy!).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(75);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(42);
    }

}
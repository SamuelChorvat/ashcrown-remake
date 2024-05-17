using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jane.Abilities;

public class FlashbangTests
{
    [Fact]
    public void FlashbangAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        var useFlashbang = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,1,1,0,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useFlashbang, useFlashbang.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        for (int i = 0; i < 3; i++)
        {
            var champion = battleLogic.GetBattlePlayer(1).Champions[i];
            champion.ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(JaneConstants.FlashbangActiveEffect).Should().BeTrue();
            var flashbangAe = champion.ActiveEffectController.GetActiveEffectByName(JaneConstants.FlashbangActiveEffect);
            flashbangAe!.TimeLeft.Should().Be(1);
            flashbangAe.Invulnerability.Should().BeTrue();
            flashbangAe.TypeOfInvulnerability!.Length.Should().Be(3);
        }
    }

    [Fact]
    public void FlashbangCanOnlyBeUsedThreeTimes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        var useFlashbang = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,1,1,0,0,0], [0,1,0,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useFlashbang, useFlashbang.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 8);
        battleLogic.AbilitiesUsed(1, useFlashbang, useFlashbang.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 8);
        battleLogic.AbilitiesUsed(1, useFlashbang, useFlashbang.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 8);
        var act = () => battleLogic.AbilitiesUsed(1, useFlashbang, useFlashbang.SpentEnergy!);

        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }
}
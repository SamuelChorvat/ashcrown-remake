using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Khan.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Khan.Abilities;

public class HandOfTheProtectorTests
{
    [Fact]
    public void HandOfTheProtectorAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        var useHandOfTheProtector = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,1,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useHandOfTheProtector, useHandOfTheProtector.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(KhanConstants.HandOfTheProtectorActiveEffect).Should().BeTrue();
        var handOfTheProtectorAe = champion.ActiveEffectController.GetActiveEffectByName(KhanConstants.HandOfTheProtectorActiveEffect);
        handOfTheProtectorAe!.TimeLeft.Should().Be(2);
        handOfTheProtectorAe.TypeOfInvulnerability![0].Should().Be(AbilityClass.All);
        handOfTheProtectorAe.Invulnerability.Should().BeTrue();
        handOfTheProtectorAe.Heal1.Should().Be(15);
    }

    [Fact]
    public void HandOfTheProtectorCorrectlyHeals()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        var useHandOfTheProtector = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var result = battleLogic.AbilitiesUsed(1, useHandOfTheProtector, useHandOfTheProtector.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(25);
    }
}
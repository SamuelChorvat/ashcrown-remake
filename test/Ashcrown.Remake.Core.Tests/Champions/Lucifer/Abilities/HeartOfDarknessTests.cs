using Ashcrown.Remake.Core.Champions.Lucifer.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lucifer.Abilities;

public class HeartOfDarknessTests
{
    [Fact]
    public void HeartOfDarknessAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useHeartOfDarkness = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,0,0,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useHeartOfDarkness, useHeartOfDarkness.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LuciferConstants.HeartOfDarknessActiveEffect).Should().BeTrue();
        var heartOfDarknessAe = champion.ActiveEffectController
            .GetActiveEffectByName(LuciferConstants.HeartOfDarknessActiveEffect);
        heartOfDarknessAe!.Infinite.Should().BeTrue();
        heartOfDarknessAe.Hidden.Should().BeTrue();
    }
}
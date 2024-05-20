using Ashcrown.Remake.Core.Champions.Khan.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Khan.Abilities;

public class BladestormTests
{
    [Fact]
    public void BladestormDealsCorrectDamageAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        var useMortalStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [0,0,1,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useMortalStrike, useMortalStrike.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        for (var i = 0; i < 3; i++)
        {
            var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[i];
            enemyChampion.Health.Should().Be(95);
            enemyChampion.ActiveEffectController
                .ActiveEffectPresentByActiveEffectName(KhanConstants.BladestormTargetActiveEffect).Should().BeTrue();
            var bladestormTargetAe = enemyChampion.ActiveEffectController
                .GetActiveEffectByName(KhanConstants.BladestormTargetActiveEffect);
            bladestormTargetAe!.TimeLeft.Should().Be(2);
        }

        var allyChampion = battleLogic.GetBattlePlayer(1).Champions[0];
        allyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(KhanConstants.BladestormMeActiveEffect).Should().BeTrue();
    }
}
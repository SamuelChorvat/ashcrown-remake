using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jafali.Abilities;

public class AvariceTests
{
    [Fact]
    public void EnvyAppliesCorrectAesAndIsReplacedByAvarice()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        var useEnvy = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [0,2,0,0]);
        var useAvarice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [0,0,0,2]);

        // Act
        var envyResult = battleLogic.AbilitiesUsed(1, useEnvy, useEnvy.SpentEnergy!);
        envyResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var avariceResult = battleLogic.AbilitiesUsed(1, useAvarice, useAvarice.SpentEnergy!);
        avariceResult.Should().BeTrue();

        // Assert
        var playerBattleInfo = battleLogic.GetBattlePlayer(2);
        playerBattleInfo.Champions[0].ActiveEffectController
            .GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect).Should().Be(2);
        playerBattleInfo.Champions[1].ActiveEffectController
            .GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect).Should().Be(2);
        playerBattleInfo.Champions[2].ActiveEffectController
            .GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect).Should().Be(2);
    }
}
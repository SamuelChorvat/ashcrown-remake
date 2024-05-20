using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Champions.Luther.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Luther.Abilities;

public class ForgeSpiritTests
{

    [Fact]
    public void ForgeSpiritAppliesCorrectAesAndDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useLivingForge = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,1,1], [1,0,0,0]);
        var useForgeSpirit = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [1,0,0,1]);

        // Act
        var livingForgeResult = battleLogic.AbilitiesUsed(1, useLivingForge, useLivingForge.SpentEnergy!);
        livingForgeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var forgeSpiritResult = battleLogic.AbilitiesUsed(1, useForgeSpirit, useForgeSpirit.SpentEnergy!);
        forgeSpiritResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(70);
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.ForgeSpiritActiveEffect).Should().BeTrue();
        var forgeSpiritAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.ForgeSpiritActiveEffect);
        forgeSpiritAe!.TimeLeft.Should().Be(3);

        var fieryBrandPresent = battleLogic.GetBattlePlayer(2).Champions.Any(c =>
            c.ActiveEffectController.ActiveEffectPresentByActiveEffectName(LutherConstants.FieryBrandTargetActiveEffect));
        fieryBrandPresent.Should().BeTrue();
    }

    [Fact]
    public void ForgeSpiritCorrectlyReducesDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(LutherConstants.Name, 
            AkioConstants.Name);
        var useLivingForge = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,1,1], [1,0,0,0]);
        var useForgeSpirit = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,0,1,0], [1,0,0,1]);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 2,
            [0,0,0,0,1,0], [0,2,0,0]);

        // Act
        var livingForgeResult = battleLogic.AbilitiesUsed(1, useLivingForge, useLivingForge.SpentEnergy!);
        livingForgeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var forgeSpiritResult = battleLogic.AbilitiesUsed(1, useForgeSpirit, useForgeSpirit.SpentEnergy!);
        forgeSpiritResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var dragonRageResult = battleLogic.AbilitiesUsed(2, useDragonRage, useDragonRage.SpentEnergy!);
        dragonRageResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var playerChampion = battleLogic.GetBattlePlayer(1).Champions[1];
        playerChampion.Health.Should().Be(70);
    }
}
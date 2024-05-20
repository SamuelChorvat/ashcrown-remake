using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Champions.Luther.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Luther.Abilities;

public class LivingForgeTests
{
    [Fact]
    public void LivingForgeAppliesCorrectAes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useLivingForge = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,1,1], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useLivingForge, useLivingForge.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var enemyChampion1 = battleLogic.GetBattlePlayer(2).Champions[0];
        var enemyChampion2 = battleLogic.GetBattlePlayer(2).Champions[1];
        var enemyChampion3 = battleLogic.GetBattlePlayer(2).Champions[2];

        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.LivingForgeMeActiveEffect).Should().BeTrue();
        var livingForgeMeAe = champion.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.LivingForgeMeActiveEffect);
        livingForgeMeAe!.TimeLeft.Should().Be(4);
        livingForgeMeAe.AllDamageReceiveModifier.Points.Should().Be(-20);

        enemyChampion1.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.LivingForgeTargetActiveEffect).Should().BeTrue();
        var livingForgeTargetAe1 = enemyChampion1.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.LivingForgeTargetActiveEffect);
        livingForgeTargetAe1!.TimeLeft.Should().Be(4);

        enemyChampion2.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.LivingForgeTargetActiveEffect).Should().BeTrue();
        var livingForgeTargetAe2 = enemyChampion2.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.LivingForgeTargetActiveEffect);
        livingForgeTargetAe2!.TimeLeft.Should().Be(4);

        enemyChampion3.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.LivingForgeTargetActiveEffect).Should().BeTrue();
        var livingForgeTargetAe3 = enemyChampion3.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.LivingForgeTargetActiveEffect);
        livingForgeTargetAe3!.TimeLeft.Should().Be(4);
    }

    [Fact]
    public void LivingForgeGivesCorrectBuffAfterStrategicAbilityIsUsed()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LutherConstants.Name);
        var useLivingForge = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,1,1], [1,0,0,0]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1,0,0,0,0,0], [0,0,1,0]);

        // Act
        var livingForgeResult = battleLogic.AbilitiesUsed(1, useLivingForge, useLivingForge.SpentEnergy!);
        livingForgeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var invulnerabilityResult = battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!);
        invulnerabilityResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LutherConstants.LivingForgeBuffActiveEffect).Should().BeTrue();
        var livingForgeBuffAe = champion.ActiveEffectController
            .GetActiveEffectByName(LutherConstants.LivingForgeBuffActiveEffect);
        livingForgeBuffAe!.AllDamageDealModifier.Points.Should().Be(5);
        livingForgeBuffAe.TimeLeft.Should().Be(1);
    }
}
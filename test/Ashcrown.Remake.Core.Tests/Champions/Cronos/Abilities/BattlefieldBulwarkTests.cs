using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Cronos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cronos.Abilities;

public class BattlefieldBulwarkTests
{
    [Fact]
    public void BattlefieldBulwarkAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        var useBattlefieldBulwark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 1, 1, 1, 1, 1], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useBattlefieldBulwark, useBattlefieldBulwark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(1).Champions)
        {
            var aeAlly = champ.ActiveEffectController.GetActiveEffectByName(CronosConstants.BattlefieldBulwarkAllyActiveEffect);
            aeAlly.Should().NotBeNull();
            aeAlly!.TimeLeft.Should().Be(2);
        }

        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            var aeEnemy = champ.ActiveEffectController.GetActiveEffectByName(CronosConstants.BattlefieldBulwarkEnemyActiveEffect);
            aeEnemy.Should().NotBeNull();
            aeEnemy!.TimeLeft.Should().Be(2);
            aeEnemy.DisableInvulnerability.Should().BeTrue();
            aeEnemy.DisableDamageReceiveReduction.Should().BeTrue();
        }
    }
    
    [Fact]
    public void BattlefieldBulwarkMakesAllyInvulnerableIfTargeted()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(CronosConstants.Name, 
            BranleyConstants.Name);
        var useBattlefieldBulwark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 1, 1, 1, 1, 1], [0, 0, 0, 1]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 1,
            [0, 0, 0, 1, 0, 0], [0, 1, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useBattlefieldBulwark, useBattlefieldBulwark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var aeInvulnerable = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CronosConstants.BattlefieldBulwarkInvulnerableActiveEffect);
        aeInvulnerable.Should().NotBeNull();
        aeInvulnerable!.TimeLeft.Should().Be(1);
        aeInvulnerable.Invulnerability.Should().BeTrue();
        aeInvulnerable.TypeOfInvulnerability.Should().BeEquivalentTo([AbilityClass.All]);
    }


}
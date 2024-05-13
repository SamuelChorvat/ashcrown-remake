using Ashcrown.Remake.Core.Champions.Gruber.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Gruber.Abilities;

public class AdaptiveVirusTests
{
    [Fact]
    public void AdaptiveVirusAppliesCorrectAes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 1, 1, 1, 1, 1], [1, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, [1, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var player1Champions = battleLogic.GetBattlePlayer(1).Champions;
        foreach (var champion in player1Champions)
        {
            champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.AdaptiveVirusAllyActiveEffect).Should().BeTrue();
            champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusAllyActiveEffect)!.DestructibleDefense.Should().Be(10);
            champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusAllyActiveEffect)!.Infinite.Should().BeTrue();
        }

        var player2Champions = battleLogic.GetBattlePlayer(2).Champions;
        foreach (var champion in player2Champions)
        {
            champion.Health.Should().Be(95);
            champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.AdaptiveVirusEnemyActiveEffect).Should().BeTrue();
            champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusEnemyActiveEffect)!.Infinite.Should().BeTrue();
        }
    }
    
    [Fact]
    public void AdaptiveVirusStacksCorrectly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(GruberConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 1, 1, 1, 1, 1], [1, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, [1, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 8);
        battleLogic.AbilitiesUsed(1, usedAbilities, [1, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var player1Champion = battleLogic.GetBattlePlayer(1).Champions[0];
        player1Champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.AdaptiveVirusAllyActiveEffect).Should().BeTrue();
        player1Champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusAllyActiveEffect)!.DestructibleDefense.Should().Be(20);
        player1Champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusAllyActiveEffect)!.Infinite.Should().BeTrue();

        var player2Champion = battleLogic.GetBattlePlayer(2).Champions[0];
        player2Champion.Health.Should().Be(70);
        player2Champion.ActiveEffectController.ActiveEffectPresentByActiveEffectName(GruberConstants.AdaptiveVirusEnemyActiveEffect).Should().BeTrue();
        player2Champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusEnemyActiveEffect)!.Infinite.Should().BeTrue();
        player2Champion.ActiveEffectController.GetActiveEffectByName(GruberConstants.AdaptiveVirusEnemyActiveEffect)!.Stacks.Should().Be(2);
    }

}
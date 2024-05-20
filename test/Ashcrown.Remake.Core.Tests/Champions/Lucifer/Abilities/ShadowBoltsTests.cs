using Ashcrown.Remake.Core.Champions.Lucifer.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lucifer.Abilities;

public class ShadowBoltsTests
{
    [Fact]
    public void ShadowBoltsDealCorrectDamageAndIgnoreInvulnerability()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useShadowBolts = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1,0,0,0,0,0], [1,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var invulnerabilityResult = battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!);
        invulnerabilityResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        var shadowBoltsResult = battleLogic.AbilitiesUsed(1, useShadowBolts, useShadowBolts.SpentEnergy!);
        shadowBoltsResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(85);
    }

    [Fact]
    public void ShadowBoltsDealCorrectDamageDuringHeartOfDarkness1()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useShadowBolts = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,0]);
        var useHeartOfDarkness = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var heartOfDarknessResult = battleLogic.AbilitiesUsed(1, useHeartOfDarkness, useHeartOfDarkness.SpentEnergy!);
        heartOfDarknessResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var shadowBoltsResult = battleLogic.AbilitiesUsed(1, useShadowBolts, useShadowBolts.SpentEnergy!);
        shadowBoltsResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(75);
    }

    [Fact]
    public void ShadowBoltsDealCorrectDamageDuringHeartOfDarkness2()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useShadowBolts = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [1,0,0,0]);
        var useHeartOfDarkness = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var firstHeartOfDarknessResult = battleLogic.AbilitiesUsed(1, useHeartOfDarkness, useHeartOfDarkness.SpentEnergy!);
        firstHeartOfDarknessResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var secondHeartOfDarknessResult = battleLogic.AbilitiesUsed(1, useHeartOfDarkness, useHeartOfDarkness.SpentEnergy!);
        secondHeartOfDarknessResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var shadowBoltsResult = battleLogic.AbilitiesUsed(1, useShadowBolts, useShadowBolts.SpentEnergy!);
        shadowBoltsResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        enemyChampions[0].Health.Should().Be(80);
        enemyChampions[1].Health.Should().Be(80);
        enemyChampions[2].Health.Should().Be(80);
    }
}
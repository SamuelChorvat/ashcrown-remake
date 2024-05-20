using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lucifer.Abilities;

public class DarkChaliceTests
{
    [Fact]
    public void DarkChaliceAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useDarkChalice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [1,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useDarkChalice, useDarkChalice.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LuciferConstants.DarkChaliceActiveEffect).Should().BeTrue();
        var darkChaliceAe = champion.ActiveEffectController
            .GetActiveEffectByName(LuciferConstants.DarkChaliceActiveEffect);
        darkChaliceAe!.Infinite.Should().BeTrue();
        darkChaliceAe.Hidden.Should().BeTrue();
    }

    [Fact]
    public void DarkChaliceDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(LuciferConstants.Name, 
            EluardConstants.Name);
        var useDarkChalice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [1,1,0,0]);
        var useSwordStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,0,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var darkChaliceResult = battleLogic.AbilitiesUsed(1, useDarkChalice, useDarkChalice.SpentEnergy!);
        darkChaliceResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var swordStrikeResult = battleLogic.AbilitiesUsed(2, useSwordStrike, useSwordStrike.SpentEnergy!);
        swordStrikeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        enemyChampions[0].Health.Should().Be(65);
        enemyChampions[1].Health.Should().Be(65);
        enemyChampions[2].Health.Should().Be(65);
    }

    [Fact]
    public void DarkChaliceDealsBonusDamageToTargetAffectedByCursedCrow()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(LuciferConstants.Name, 
            EluardConstants.Name);
        var useDarkChalice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [1,1,0,0]);
        var useCursedCrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,0,0,0]);
        var useSwordStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,0,1,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var darkChaliceResult = battleLogic.AbilitiesUsed(1, useDarkChalice, useDarkChalice.SpentEnergy!);
        darkChaliceResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var cursedCrowResult = battleLogic.AbilitiesUsed(1, useCursedCrow, useCursedCrow.SpentEnergy!);
        cursedCrowResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var swordStrikeResult = battleLogic.AbilitiesUsed(2, useSwordStrike, useSwordStrike.SpentEnergy!);
        swordStrikeResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var enemyChampions = battleLogic.GetBattlePlayer(2).Champions;
        enemyChampions[0].Health.Should().Be(55);
        enemyChampions[1].Health.Should().Be(65);
        enemyChampions[2].Health.Should().Be(65);
    }
}
using Ashcrown.Remake.Core.Champions.Lucifer.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Lucifer.Abilities;

public class CursedCrowTests
{
    [Fact]
    public void CursedCrowAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useDarkChalice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [1,1,0,0]);
        var useCursedCrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var darkChaliceResult = battleLogic.AbilitiesUsed(1, useDarkChalice, useDarkChalice.SpentEnergy!);
        darkChaliceResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var cursedCrowResult = battleLogic.AbilitiesUsed(1, useCursedCrow, useCursedCrow.SpentEnergy!);
        cursedCrowResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(LuciferConstants.CursedCrowActiveEffect).Should().BeTrue();
        var cursedCrowAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(LuciferConstants.CursedCrowActiveEffect);
        cursedCrowAe!.Hidden.Should().BeTrue();
        cursedCrowAe.Infinite.Should().BeTrue();
        cursedCrowAe.Stacks.Should().Be(1);
    }

    [Fact]
    public void CursedCrowStacks()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(LuciferConstants.Name);
        var useDarkChalice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [1,1,0,0]);
        var useCursedCrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,0,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        var darkChaliceResult = battleLogic.AbilitiesUsed(1, useDarkChalice, useDarkChalice.SpentEnergy!);
        darkChaliceResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var firstCursedCrowResult = battleLogic.AbilitiesUsed(1, useCursedCrow, useCursedCrow.SpentEnergy!);
        firstCursedCrowResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var secondCursedCrowResult = battleLogic.AbilitiesUsed(1, useCursedCrow, useCursedCrow.SpentEnergy!);
        secondCursedCrowResult.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        var cursedCrowAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(LuciferConstants.CursedCrowActiveEffect);
        cursedCrowAe!.Stacks.Should().Be(2);
    }
}
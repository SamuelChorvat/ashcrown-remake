using Ashcrown.Remake.Core.Champions.Dex.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Dex.Abilities;

public class ShurikenThrowTests
{
    [Fact]
    public void ShurikenThrowDealsCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DexConstants.Name);
        var useShurikenThrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 1, 1], [0, 0, 0, 2]);

        // Act
        battleLogic.AbilitiesUsed(1, useShurikenThrow, useShurikenThrow.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            champ.Health.Should().Be(85);
        }
    }
    
    [Fact]
    public void ShurikenThrowsCostIsReducedWhenNightbladeIsActive()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(DexConstants.Name);
        var useNightblade = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 1]);
        var useShurikenThrow = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 1, 1], [0, 0, 0, 1]);

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(1).GetCurrentCost()[4].Should().Be(2);
        battleLogic.AbilitiesUsed(1, useNightblade, useNightblade.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(1).GetCurrentCost()[4].Should().Be(1);
        battleLogic.AbilitiesUsed(1, useShurikenThrow, useShurikenThrow.SpentEnergy!).Should().BeTrue();
    }


}
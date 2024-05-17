using Ashcrown.Remake.Core.Champions.Jafali.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jafali.Abilities;

public class PrideTests
{
    [Fact]
    public void PrideDealsCorrectDamageAndDoesntRemoveEnergyIfNoDecayingSoulsPresent()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        var usePride = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,2,0,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usePride, usePride.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).GetTotalEnergy().Should().Be(40);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(43);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
    }

    [Fact]
    public void PrideRemovesEnergyEqualToNumberOfDecayingSoulsOnTarget()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JafaliConstants.Name);
        var useAnger = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,0]);
        var useDecayingSoul = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,0,1,0]);
        var usePride = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0,0,0,1,0,0], [0,2,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useAnger, useAnger.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useDecayingSoul, useDecayingSoul.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(46);
        var result = battleLogic.AbilitiesUsed(1, usePride, usePride.SpentEnergy!);
        result.Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(46 - battleLogic.GetBattlePlayer(2).Champions[0]
            .ActiveEffectController.GetActiveEffectCountByName(JafaliConstants.DecayingSoulActiveEffect));
    }
}

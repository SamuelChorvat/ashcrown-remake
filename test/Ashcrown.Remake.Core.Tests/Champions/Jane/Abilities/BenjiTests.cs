using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Jane.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Jane.Abilities;

public class BenjiTests
{
    [Fact]
    public void BenjiAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(JaneConstants.BenjiActiveEffect).Should().BeTrue();
        var benjiAe = champion.ActiveEffectController.GetActiveEffectByName(JaneConstants.BenjiActiveEffect);
        benjiAe!.TimeLeft.Should().Be(4);
        benjiAe.AllDamageReceiveModifier.Points.Should().Be(-10);
    }

    [Fact]
    public void BenjiIsReplacedByGoForTheThroat()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(JaneConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,1,0,0]);

        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(1).Name.Should().Be("Benji");
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(1).Name.Should().Be("Go for the Throat");
    }

    [Fact]
    public void BenjiDealsDamageWhenTargetedByHarmfulAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(JaneConstants.Name, 
            BranleyConstants.Name);
        var useBenji = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1,0,0,0,0,0], [0,1,0,0]);
        var usePlunder = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,1,0]);

        battleLogic.AbilitiesUsed(1, useBenji, useBenji.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, usePlunder, usePlunder.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
    }
}
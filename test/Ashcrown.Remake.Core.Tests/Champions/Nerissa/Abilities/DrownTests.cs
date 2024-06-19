using Ashcrown.Remake.Core.Champions.Nerissa.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nerissa.Abilities;

public class DrownTests
{
    [Fact]
    public void DrownAppliesCorrectAeAndDealsDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        var useDrown = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useDrown, useDrown.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(NerissaConstants.DrownActiveEffect).Should().BeTrue();
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(NerissaConstants.DrownActiveEffect)!;
        ae.Infinite.Should().BeTrue();
        ae.AfflictionDamage.Should().BeTrue();
        ae.Damage1.Should().Be(10);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
    }

    [Fact]
    public void DrownCannotBeUsedOnEnemyAlreadyAffectedByIt()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        var useDrown = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0,0,0,1,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useDrown, useDrown.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);

        // Assert
        result = battleLogic.AbilitiesUsed(1, useDrown, useDrown.SpentEnergy!);
        result.Should().BeFalse();
    }
}
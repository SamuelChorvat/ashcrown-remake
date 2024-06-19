using Ashcrown.Remake.Core.Champions.Nerissa.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Nerissa.Abilities;

public class AncientSpiritsTests
{
    [Fact]
    public void AncientSpiritsHealsCorrectAmountAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;
        var useAncientSpirits = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1,0,0,0,0,0], [0,0,1,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useAncientSpirits, useAncientSpirits.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(35);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(NerissaConstants.AncientSpiritsActiveEffect).Should().BeTrue();
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(NerissaConstants.AncientSpiritsActiveEffect)!;
        ae.Infinite.Should().BeTrue();
        ae.DestructibleDefense.Should().Be(25);
    }

    [Fact]
    public void AncientSpiritsStacksCorrectly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(NerissaConstants.Name);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;
        var useAncientSpirits = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1,0,0,0,0,0], [0,0,1,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useAncientSpirits, useAncientSpirits.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 8);

        result = battleLogic.AbilitiesUsed(1, useAncientSpirits, useAncientSpirits.SpentEnergy!);
        result.Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(NerissaConstants.AncientSpiritsActiveEffect).Should().BeTrue();
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(NerissaConstants.AncientSpiritsActiveEffect)!;
        ae.Infinite.Should().BeTrue();
        ae.DestructibleDefense.Should().Be(50);
    }
}
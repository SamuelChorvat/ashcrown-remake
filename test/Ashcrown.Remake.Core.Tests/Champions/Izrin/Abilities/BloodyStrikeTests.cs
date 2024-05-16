using Ashcrown.Remake.Core.Champions.Izrin.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Izrin.Abilities;

public class BloodyStrikeTests
{
    [Fact]
    public void BloodyStrikeDealsCorrectDamageAndAppliesAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(IzrinConstants.BloodyStrikeActiveEffect).Should().BeTrue();
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(IzrinConstants.BloodyStrikeActiveEffect);
        ae!.TimeLeft.Should().Be(1);
        ae.AllDamageReceiveModifier.Points.Should().Be(-10);
    }

    [Fact]
    public void BloodyStrikeAppliesHelperAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [0,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(IzrinConstants.BloodyStrikeHelperActiveEffect).Should().BeTrue();
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(IzrinConstants.BloodyStrikeHelperActiveEffect);
        ae!.AllDamageReceiveModifier.Points.Should().Be(0);
    }

    [Fact]
    public void BloodyStrikeDealsBonusDamageAndGivesMoreDamageReductionIfUsedTwoTurnInRow()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [0,1,0,0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(50);
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(IzrinConstants.BloodyStrikeActiveEffect);
        ae!.TimeLeft.Should().Be(1);
        ae.AllDamageReceiveModifier.Points.Should().Be(-20);
    }

    [Fact]
    public void BloodyStrikeDealsBonusDamageForDeadAllies()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(IzrinConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [0,1,0,0]);
        battleLogic.GetBattlePlayer(1).Champions[1].Health = 0;
        battleLogic.GetBattlePlayer(1).Champions[1].Alive = false;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(IzrinConstants.BloodyStrikeActiveEffect);
        ae!.TimeLeft.Should().Be(1);
        ae.AllDamageReceiveModifier.Points.Should().Be(-10);
    }
}
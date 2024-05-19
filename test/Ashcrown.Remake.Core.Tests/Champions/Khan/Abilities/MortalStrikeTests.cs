using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Khan.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Khan.Abilities;

public class MortalStrikeTests
{
    [Fact]
    public void MortalStrikeDealsCorrectDamageAndAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        var useMortalStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,1]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useMortalStrike, useMortalStrike.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var enemyChampion = battleLogic.GetBattlePlayer(2).Champions[0];
        enemyChampion.Health.Should().Be(65);
        enemyChampion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(KhanConstants.MortalStrikeActiveEffect).Should().BeTrue();
        var mortalStrikeAe = enemyChampion.ActiveEffectController
            .GetActiveEffectByName(KhanConstants.MortalStrikeActiveEffect);
        mortalStrikeAe!.TimeLeft.Should().Be(2);
        mortalStrikeAe.HealingDealModifier.Percentage.Should().Be(-25);
        mortalStrikeAe.HealingReceiveModifier.Percentage.Should().Be(-50);
    }

    [Fact]
    public void MortalStrikeCorrectlyReducesHealing()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(KhanConstants.Name, 
            AlthalosConstants.Name);
        var useMortalStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,1]);
        var useHolyLight = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1,0,0,0,0,0], [1,0,0,0]);

        // Act
        battleLogic.AbilitiesUsed(1, useMortalStrike, useMortalStrike.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHolyLight, useHolyLight.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(74);
    }

    [Fact]
    public void MortalStrikeDealsBonusDamageDuringBladestorm()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        var useBladestorm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,1,1], [0,0,1,0]);
        var useMortalStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useBladestorm, useBladestorm.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(95);
        var result = battleLogic.AbilitiesUsed(1, useMortalStrike, useMortalStrike.SpentEnergy!);
        result.Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(50);
    }

    [Fact]
    public void MortalStrikeDealsLessDamageDuringHandOfTheProtector()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(KhanConstants.Name);
        var useHandOfProtector = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1,0,0,0,0,0], [0,0,1,0]);
        var useMortalStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0,0,0,1,0,0], [0,1,0,1]);

        // Act
        battleLogic.AbilitiesUsed(1, useHandOfProtector, useHandOfProtector.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        var result = battleLogic.AbilitiesUsed(1, useMortalStrike, useMortalStrike.SpentEnergy!);
        result.Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(75);
    }
}
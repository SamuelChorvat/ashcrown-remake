using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Evanore.Abilities;

public class UmbraWolfTests
{
    [Fact]
    public void UmbraWolfAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EvanoreConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var umbraWolfAe = champion.ActiveEffectController.GetActiveEffectByName(EvanoreConstants.UmbraWolfActiveEffect);
        umbraWolfAe.Should().NotBeNull();
        umbraWolfAe!.TimeLeft.Should().Be(4);
        umbraWolfAe.AllDamageReceiveModifier.Percentage.Should().Be(-50);
    }
    
    [Fact]
    public void UmbraWolfCorrectlyReducesDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(EvanoreConstants.Name, 
            AlthalosConstants.Name);
        var useUmbraWolf = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useUmbraWolf, useUmbraWolf.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var umbraWolfAe = champion.ActiveEffectController
            .GetActiveEffectByName(EvanoreConstants.UmbraWolfActiveEffect);

        champion.Health.Should().Be(90);
        umbraWolfAe.Should().NotBeNull();
        umbraWolfAe!.TimeLeft.Should().Be(3);
        umbraWolfAe.AllDamageReceiveModifier.Percentage.Should().Be(-50);
    }
    
    [Fact]
    public void UmbraWolfCorrectlyIgnoresStun()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(EvanoreConstants.Name, 
            AlthalosConstants.Name);
        var useUmbraWolf = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);
        var useHoundDefense = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1, 0, 0, 0, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useUmbraWolf, useUmbraWolf.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.AbilitiesUsed(1, useHoundDefense, useHoundDefense.SpentEnergy!).Should().BeTrue();
    }
}
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Evanore.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Evanore.Abilities;

public class MonstrousBearTests
{
    [Fact]
    public void MonstrousBearDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EvanoreConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 1, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion2 = battleLogic.GetBattlePlayer(2).Champions[0];

        champion2.Health.Should().Be(70);
        var monstrousBearAe = champion2.ActiveEffectController.GetActiveEffectByName(EvanoreConstants.MonstrousBearActiveEffect);
        monstrousBearAe.Should().NotBeNull();
        monstrousBearAe!.TimeLeft.Should().Be(2);
    }
    
    [Fact]
    public void MonstrousBearDealsDamageWhenTargetUsesAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(EvanoreConstants.Name, 
            AlthalosConstants.Name);
        var useMonstrousBear = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 1, 0, 1]);
        var useHammerOfJustice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useMonstrousBear, useMonstrousBear.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var champion2 = battleLogic.GetBattlePlayer(2).Champions[0];
        champion2.Health.Should().Be(70);
        battleLogic.AbilitiesUsed(2, useHammerOfJustice, useHammerOfJustice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        champion2.Health.Should().Be(55);
        var monstrousBearAe = champion2.ActiveEffectController.GetActiveEffectByName(EvanoreConstants.MonstrousBearActiveEffect);
        monstrousBearAe.Should().NotBeNull();
        monstrousBearAe!.TimeLeft.Should().Be(2);
    }
}
// ReSharper disable InconsistentNaming

using Ashcrown.Remake.Core.Champions.Cronos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cronos.Abilities;

public class EMPBurstTests
{
    [Fact]
    public void EMPBurstDealsCorrectDamageAppliesActiveEffectAndSwapsAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 1, 1, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var currentAbility = battleLogic.GetBattlePlayer(1).Champions[0].AbilityController.GetCurrentAbility(3);
        currentAbility.Name.Should().Be(CronosConstants.PulseCannon);

        foreach (var champ in battleLogic.GetBattlePlayer(2).Champions)
        {
            champ.Health.Should().Be(75);
            var ae = champ.ActiveEffectController.GetActiveEffectByName(CronosConstants.EMPBurstActiveEffect);
            ae.Should().NotBeNull();
            ae!.Infinite.Should().BeTrue();
        }
    }
    
    [Fact]
    public void EMPBurstDebuffIncreasesAfflictionDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CronosConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [0, 0, 0, 1, 1, 1], [0, 1, 1, 1]);
        var usedGravityWell = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, usedGravityWell, usedGravityWell.SpentEnergy!).Should().BeTrue();

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(50);
        battleLogic.GetBattlePlayer(2).Champions[1].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(60);
    }
}
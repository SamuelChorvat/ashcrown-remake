using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Branley.Abilities;

public class FireTheCannonsTests
{
    [Fact]
    public void FireTheCannonsDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        var useFireTheCannons = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useFireTheCannons, useFireTheCannons.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        for (var i = 0; i < 3; i++)
        {
            var champion = battleLogic.GetBattlePlayer(2).Champions[i];
            champion.Health.Should().Be(90);
            var ae = champion.ActiveEffectController.GetActiveEffectByName(BranleyConstants.FireTheCannonsActiveEffect);
            ae.Should().NotBeNull();
            ae!.TimeLeft.Should().Be(1);
            ae.Stun.Should().BeTrue();
            ae.StunType.Should().BeEquivalentTo(new[] {AbilityClass.Physical, AbilityClass.Affliction});
        }
    }
    
    [Fact]
    public void FireTheCannonsStunsCorrectAbilities()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(BranleyConstants.Name);
        var useFireTheCannons = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 1, 1], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useFireTheCannons, useFireTheCannons.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        for (var i = 0; i < battleLogic.GetBattlePlayer(2).Champions.Length; i++)
        {
            var champion = battleLogic.GetBattlePlayer(2).Champions[i];
            var abilityControl = champion.AbilityController;
            var currentAbility = abilityControl.GetCurrentAbility(i + 1);
            if (currentAbility.AbilityClassesContains(AbilityClass.Physical))
            {
                abilityControl.IsStunnedToUseAbility(currentAbility).Should().BeTrue();
            }
        }
    }
}
using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Ability;

public class StandardInvulnerabiltyTests
{
    [Theory]
    [InlineData(AlthalosConstants.Althalos, AlthalosConstants.DivineShieldActiveEffect)]
    [InlineData(EluardConstants.Eluard, EluardConstants.EvadeActiveEffect)]
    public void InvulnerabilityShouldCorrectlyApplyActiveEffects(string championName, string activeEffectName)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(championName);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,4, 
            [1,0,0,0,0,0], [0,1,0,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(activeEffectName).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(activeEffectName)!.Invulnerability.Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(activeEffectName)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(activeEffectName)!.TypeOfInvulnerability![0].Should().Be(AbilityClass.All);
    }

    [Theory]
    [InlineData(AlthalosConstants.Althalos, AlthalosConstants.DivineShieldActiveEffect)]
    [InlineData(EluardConstants.Eluard, EluardConstants.EvadeActiveEffect)]
    public void InvulnerabilityShouldBeInvulnerability(string championName, string activeEffectName)
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(championName, 
            AlthalosConstants.Althalos);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,4, 
            [1,0,0,0,0,0], [0,1,0,0]);
        var useDamage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,1, 
            [0,0,0,1,0,0], [0,0,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useDamage, useDamage.SpentEnergy!).Should().BeFalse();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(activeEffectName).Should().BeFalse();
    }
}
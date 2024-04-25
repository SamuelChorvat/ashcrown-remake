using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Eluard.Abilities;

public class DevastateTests
{
    [Fact]
    public void DevastateShouldNotBeUsableWithoutUnyieldingWill()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Name);
        var useDevastate = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,2, 
            [0,0,0,1,0,0], [0,1,1,0]);
        
        // Act
        var act = () => battleLogic.AbilitiesUsed(1, useDevastate, useDevastate.SpentEnergy!);
        
        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }

    [Fact]
    public void DevastateDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(EluardConstants.Name);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,3, 
            [1,0,0,0,0,0], [0,0,1,0]);
        var useDevastate = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1,2, 
            [0,0,0,1,0,0], [0,1,1,0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useUnyieldingWill, useUnyieldingWill.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useDevastate, useDevastate.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(55);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(EluardConstants.DevastateActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.DevastateActiveEffect)!.Stun.Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.DevastateActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.DevastateActiveEffect)!.StunType![0].Should().Be(AbilityClass.All);
    }
}
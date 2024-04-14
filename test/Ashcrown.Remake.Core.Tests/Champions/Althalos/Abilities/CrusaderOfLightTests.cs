using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace Ashcrown.Remake.Core.Tests.Champions.Althalos.Abilities;

public class CrusaderOfLightTests
{
    [Fact]
    public void CrusaderOfLightShouldApplyCorrectActiveEffect()
    {
        //Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AlthalosConstants.Althalos);
        var endTurn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [1, 0, 0, 0, 0, 0], [1,0,0,0]);
        
        //Act
        if (!battleLogic.AbilitiesUsed(1, endTurn, endTurn.SpentEnergy!)) {
            throw new XunitException("Ability was not used!");
        }
        BattleTestHelpers.PassNumberOfTurn(1, battleLogic, 1);
        
        //Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AlthalosConstants.CrusaderOfLightActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AlthalosConstants.CrusaderOfLightActiveEffect)!.IgnoreStuns.Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AlthalosConstants.CrusaderOfLightActiveEffect)!.TimeLeft.Should().Be(4);
    }
}
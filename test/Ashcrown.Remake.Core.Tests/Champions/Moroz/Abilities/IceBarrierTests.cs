using Ashcrown.Remake.Core.Champions.Branley.Champion;
using Ashcrown.Remake.Core.Champions.Hannibal.Champion;
using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Moroz.Abilities;

public class IceBarrierTests
{
    [Fact]
    public void IceBarrierAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        var useIceBarrier = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [1,0,0,0,0,0], [0,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useIceBarrier, useIceBarrier.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.IceBarrierActiveEffect).Should().BeTrue();
        var iceBarrierAe = champion.ActiveEffectController
            .GetActiveEffectByName(MorozConstants.IceBarrierActiveEffect);
        iceBarrierAe!.Infinite.Should().BeTrue();
    }

    [Fact]
    public void IceBarrierCorrectlyIgnoresStun()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(MorozConstants.Name, 
            HannibalConstants.Name);
        var useIceBarrier = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [1,0,0,0,0,0], [0,0,0,0]);
        var useHealthFunnel = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [0,0,0,1,0,0], [1,0,0,1]);
        var useFreeze = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, 
            [0,0,0,1,0,0], [1,1,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useIceBarrier, useIceBarrier.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var resultHealthFunnel = battleLogic.AbilitiesUsed(2, useHealthFunnel, useHealthFunnel.SpentEnergy!);
        resultHealthFunnel.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.IceBarrierActiveEffect).Should().BeTrue();
        var freezeResult = battleLogic.AbilitiesUsed(1, useFreeze, useFreeze.SpentEnergy!);
        freezeResult.Should().BeTrue();
    }

    [Fact]
    public void IceBarrierIsRemovedAfterBeingTargetOfStrategicAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(MorozConstants.Name, 
            BranleyConstants.Name);
        var useIceBarrier = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [1,0,0,0,0,0], [0,0,0,0]);
        var useRaiseTheFlag = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(2, 3, 
            [0,0,0,1,1,1], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useIceBarrier, useIceBarrier.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        var raiseFlagResult = battleLogic.AbilitiesUsed(2, useRaiseTheFlag, useRaiseTheFlag.SpentEnergy!);
        raiseFlagResult.Should().BeTrue();

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.IceBarrierActiveEffect).Should().BeFalse();
    }
}
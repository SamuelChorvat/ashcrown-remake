using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Akio.Abilities;

public class MasterfulSliceTests
{
    [Fact]
    public void MasterfulSliceShouldApplyActiveEffectWithCorrectDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useMasterfulSlice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useMasterfulSlice, useMasterfulSlice.SpentEnergy!).Should().BeTrue();
        
        // Assert
        var activeEffect = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AkioConstants.MasterfulSliceActiveEffect);

        activeEffect.Should().NotBeNull();
        activeEffect!.TimeLeft.Should().Be(3);
        activeEffect.PiercingDamage.Should().BeFalse();
    }

    [Fact]
    public void MasterfulSliceShouldDealCorrectDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useMasterfulSlice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useMasterfulSlice, useMasterfulSlice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.MasterfulSliceActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AkioConstants.MasterfulSliceActiveEffect)!.TimeLeft.Should().Be(2);
    }

    [Fact]
    public void MasterfulSliceShouldDealCorrectDamageOverTheWholeDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useMasterfulSlice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useMasterfulSlice, useMasterfulSlice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.MasterfulSliceActiveEffect).Should().BeFalse();
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(70);
    }

    [Fact]
    public void MasterfulSliceShouldDealCorrectDamageIfEnemyUsesNonStrategicAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useMasterfulSlice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useMasterfulSlice, useMasterfulSlice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useDragonRage, useDragonRage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.MasterfulSliceActiveEffect).Should().BeTrue();
    }

    [Fact]
    public void MasterfulSliceShouldNotDealDamageIfEnemyUsesStrategicAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useMasterfulSlice = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [1, 0, 0, 0]);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);
        battleLogic.GetBattlePlayer(2).Champions[0].CurrentAbilities[1].AbilityClasses =
        [
            AbilityClass.Strategic
        ];

        // Act
        battleLogic.AbilitiesUsed(1, useMasterfulSlice, useMasterfulSlice.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useDragonRage, useDragonRage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.MasterfulSliceActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(90);
    }
}
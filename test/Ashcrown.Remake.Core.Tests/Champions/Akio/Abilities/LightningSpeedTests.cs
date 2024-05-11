using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Akio.Abilities;

public class LightningSpeedTests
{
    [Fact]
    public void LightningSpeedShouldCorrectlyApplyActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useLightningSpeed = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useLightningSpeed, useLightningSpeed.SpentEnergy!).Should().BeTrue();
        
        // Assert
        var activeEffect = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AkioConstants.LightningSpeedActiveEffect);

        activeEffect.Should().NotBeNull();
        activeEffect!.Hidden.Should().BeTrue();
        activeEffect.TimeLeft.Should().Be(1);
        activeEffect.IgnoreHarmful.Should().BeTrue();
    }

    [Fact]
    public void LightningSpeedShouldIgnoreHarmfulAndIncreaseDragonRageDamageIfTriggered()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useLightningSpeed = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);
        var useTwoHarmful = BattleTestHelpers.CreateEndTurnWithTwoAbilitiesUsed(1, 2, [0, 0, 0, 1, 0, 0],
                2, 2, [0, 0, 0, 1, 0, 0], [0, 4, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useLightningSpeed, [0, 1, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useTwoHarmful, useTwoHarmful.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(100);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.LightningSpeedEndActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.DragonRageActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AkioConstants.DragonRageActiveEffect)!.Stacks.Should().Be(2);
    }
}
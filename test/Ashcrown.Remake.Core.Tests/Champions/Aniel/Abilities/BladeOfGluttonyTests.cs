using Ashcrown.Remake.Core.Ability.Enums;
using Ashcrown.Remake.Core.Champions.Aniel.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Aniel.Abilities;

public class BladeOfGluttonyTests
{
    [Fact]
    public void BladeOfGluttonyShouldRemoveRandomEnergyAndApplyCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [0,0,0,1,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Blue] = 1;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Red] = 0;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Green] = 0;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Purple] = 0;
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(1);

        // Act
        battleLogic.AbilitiesUsed(1, useAbilities, useAbilities.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(3);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.BladeOfGluttonyUsedActiveEffect)!.TimeLeft.Should().Be(1);
    }

    [Fact]
    public void BladeOfGluttonyShouldApplyCorrectActiveEffectIfUsedOnTargetAffectedByCondemn()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useCondemn = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, [0,0,0,1,0,0], [0,0,0,1]);
        var useBladeOfGluttony = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [0,0,0,1,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Blue] = 1;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Red] = 0;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Green] = 0;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Purple] = 0;

        // Act
        battleLogic.AbilitiesUsed(1, useCondemn, useCondemn.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(4);
        battleLogic.AbilitiesUsed(1, useBladeOfGluttony, useBladeOfGluttony.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(6);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.BladeOfGluttonyUsedMagicActiveEffect)!.MagicDamageReceiveModifier.Points.Should().Be(15);
    }

    [Fact]
    public void BladeOfGluttonyShouldApplyCorrectActiveEffectIfUsedOnTargetAffectedByEnchantedGarlicBomb()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AnielConstants.Name);
        var useEnchantedGarlicBomb = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [0,0,0,1,0,0], [0,0,0,1]);
        var useBladeOfGluttony = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [0,0,0,1,0,0], [0,0,0,1]);
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Blue] = 1;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Red] = 0;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Green] = 0;
        battleLogic.GetBattlePlayer(2).Energy[(int) EnergyType.Purple] = 0;

        // Act
        battleLogic.AbilitiesUsed(1, useEnchantedGarlicBomb, [0,0,0,1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(4);
        battleLogic.AbilitiesUsed(1, useBladeOfGluttony, [0,0,0,1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).GetTotalEnergy().Should().Be(6);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AnielConstants.BladeOfGluttonyUsedPhysicalActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.BladeOfGluttonyUsedPhysicalActiveEffect)!.TimeLeft.Should().Be(1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AnielConstants.BladeOfGluttonyUsedPhysicalActiveEffect)!.PhysicalDamageReceiveModifier.Points.Should().Be(15);
    }
}
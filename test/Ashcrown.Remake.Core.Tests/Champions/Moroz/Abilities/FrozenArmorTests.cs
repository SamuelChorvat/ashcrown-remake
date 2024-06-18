using Ashcrown.Remake.Core.Champions.Moroz.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Moroz.Abilities;

public class FrozenArmorTests
{
    [Fact]
    public void FrozenArmorAppliesCorrectAe()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        var useFrozenArmor = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1,0,0,0,0,0], [1,0,0,0]);
        
        // Act
        var result = battleLogic.AbilitiesUsed(1, useFrozenArmor, useFrozenArmor.SpentEnergy!);
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        champion.ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(MorozConstants.FrozenArmorActiveEffect).Should().BeTrue();
        var frozenArmorAe = champion.ActiveEffectController
            .GetActiveEffectByName(MorozConstants.FrozenArmorActiveEffect);
        frozenArmorAe!.Infinite.Should().BeTrue();
        frozenArmorAe.DestructibleDefense.Should().Be(40);
    }

    [Fact]
    public void FrozenArmorReapplies()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(MorozConstants.Name);
        var useFrozenArmor = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, 
            [1,0,0,0,0,0], [1,0,0,0]);

        // Act
        var result = battleLogic.AbilitiesUsed(1, useFrozenArmor, useFrozenArmor.SpentEnergy!);
        result.Should().BeTrue();
        var champion = battleLogic.GetBattlePlayer(1).Champions[0];
        var frozenArmorAe = champion.ActiveEffectController
            .GetActiveEffectByName(MorozConstants.FrozenArmorActiveEffect);
        frozenArmorAe!.DestructibleDefense = 10;
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 10);
        frozenArmorAe.DestructibleDefense.Should().Be(10);
        var reapplyResult = battleLogic.AbilitiesUsed(1, useFrozenArmor, useFrozenArmor.SpentEnergy!);
        reapplyResult.Should().BeTrue();

        // Assert
        champion.ActiveEffectController.GetActiveEffectCountByName(MorozConstants.FrozenArmorActiveEffect).Should().Be(1);
        champion.ActiveEffectController
            .GetActiveEffectByName(MorozConstants.FrozenArmorActiveEffect)!.DestructibleDefense.Should().Be(40);
    }
}
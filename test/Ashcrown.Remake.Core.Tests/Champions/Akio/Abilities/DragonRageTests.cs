using Ashcrown.Remake.Core.Champions.Akio.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Akio.Abilities;

public class DragonRageTests
{
    [Fact]
    public void DragonRageShouldDealCorrectDamageAndApplyActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);
        
        // Act
        battleLogic.AbilitiesUsed(1, useDragonRage, useDragonRage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AkioConstants.DragonRageActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AkioConstants.DragonRageActiveEffect)!.Infinite.Should().BeTrue();
    }

    [Fact]
    public void DragonRageShouldDealPiercingDamage()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(AkioConstants.Name, 
            EluardConstants.Name);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(3, 3,
            [0, 0, 1, 0, 0, 0], [0, 1, 0, 0]);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 0, 0, 1], [0, 2, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useUnyieldingWill, [0, 1, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.AbilitiesUsed(1, useDragonRage, [0, 2, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(60);
    }
    
    [Fact]
    public void DragonRageShouldDealPiercingDamageDestructible()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(AkioConstants.Name, 
            EluardConstants.Name);
        var useUnyieldingWill = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(3, 3,
            [0, 0, 1, 0, 0, 0], [0, 1, 0, 0]);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 0, 0, 1], [0, 2, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useUnyieldingWill, [0, 1, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[2].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.UnyieldingWillActiveEffect)!.DestructibleDefense = 35;
        battleLogic.AbilitiesUsed(1, useDragonRage, [0, 2, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(95);
        battleLogic.GetBattlePlayer(2).Champions[2].ActiveEffectController
            .GetActiveEffectByName(EluardConstants.UnyieldingWillActiveEffect)!
            .DestructibleDefense.Should().Be(0);
    }

    [Fact]
    public void DragonRageShouldIgnoreInvulnerability()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useFlowDisruption = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4,
            [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);
        var useDragonRage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(2, useFlowDisruption, [0, 1, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.AbilitiesUsed(1, useDragonRage, [0, 2, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        
        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }

    [Fact]
    public void DragonRageShouldCorrectlyIncreaseItsDamageEachTimeItIsUsed()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AkioConstants.Name);
        var useDragonRage1 = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 1, 0, 0], [0, 2, 0, 0]);
        var useDragonRage2 = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 0, 1, 0], [0, 2, 0, 0]);
        var useDragonRage3 = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0, 0, 0, 0, 0, 1], [0, 2, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useDragonRage1, [0, 2, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useDragonRage2, [0, 2, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useDragonRage3, [0, 2, 0, 0]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        
        // Assert
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AkioConstants.DragonRageActiveEffect)!.Stacks.Should().Be(3);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
        battleLogic.GetBattlePlayer(2).Champions[1].Health.Should().Be(55);
        battleLogic.GetBattlePlayer(2).Champions[2].Health.Should().Be(50);
    }
}
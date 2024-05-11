using Ashcrown.Remake.Core.Champions.Althalos.Champion;
using Ashcrown.Remake.Core.Champions.Azrael.Champion;
using Ashcrown.Remake.Core.Champions.Eluard.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Azrael.Abilities;

public class ReapTests
{
    [Fact]
    public void ReapDealsCorrectDamageAndAppliesActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useReap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [0, 0, 0, 1, 0, 0], 
            [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useReap, useReap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
        var ae = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.ReapTargetActiveEffect);
        ae.Should().NotBeNull();
        ae!.TimeLeft.Should().Be(1);
    }
    
    [Fact]
    public void ReapDealsBonusDamageIfUsedOnSameTargetAndTheyUsedAbility()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithTwoDifferentChampions(AzraelConstants.Name, 
            EluardConstants.Name);
        var useReap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        var useSwordStrike = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 1, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useReap, useReap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.AbilitiesUsed(2, useSwordStrike, useSwordStrike.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AzraelConstants.ReapTriggeredTargetActiveEffect).Should().BeTrue();
        battleLogic.AbilitiesUsed(1, useReap, useReap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(50);
    }
    
    [Fact]
    public void ReapDealsBonusDamageIfUsedAfterSoulRealm()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useSoulRealm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, [1, 0, 0, 0, 0, 0], [0, 0, 0, 1]);
        var useReap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useSoulRealm, useSoulRealm.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useReap, useReap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }
    
    [Fact]
    public void ReapIgnoresInvulnerability()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useInvulnerability = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 4, [1, 0, 0, 0, 0, 0], 
        [0, 0, 0, 1]);
        var useReap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [0, 0, 0, 1, 0, 0], 
            [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(2, useInvulnerability, useInvulnerability.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(2, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(AzraelConstants.DisappearActiveEffect).Should().BeTrue();
        battleLogic.AbilitiesUsed(1, useReap, useReap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(80);
    }

    [Fact]
    public void ReapDealsBonusDamageAfterCursedMark()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useCursedMark = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3, [1, 0, 0, 0, 0, 0], [0, 1, 0, 0]);
        var useReap = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useCursedMark, useCursedMark.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useReap, useReap.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(60);
    }
}

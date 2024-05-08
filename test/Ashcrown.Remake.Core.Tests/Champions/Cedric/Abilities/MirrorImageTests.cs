using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cedric.Abilities;

public class MirrorImageTests
{
    [Fact]
    public void MirrorImageAppliesCorrectActiveEffect()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useMirrorImage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2, 
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useMirrorImage, useMirrorImage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.DestructibleDefense.Should().Be(25);
        ae.Stacks.Should().Be(1);
    }
    
    [Fact]
    public void MirrorImageStacksCorrectly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useMirrorImage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useMirrorImage, useMirrorImage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 4);
        battleLogic.AbilitiesUsed(1, useMirrorImage, [0, 0, 0, 1]).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var ae = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.MirrorImageActiveEffect);
        ae.Should().NotBeNull();
        ae!.Infinite.Should().BeTrue();
        ae.DestructibleDefense.Should().Be(50);
        ae.Stacks.Should().Be(2);
    }
    
    [Fact]
    public void MirrorImageIncreasesDurationOfDarkSoul()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useMirrorImage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 1]);
        var useDarkSoul = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useMirrorImage, useMirrorImage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useDarkSoul, useDarkSoul.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var aeDarkSoul = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.DarkSoulTargetActiveEffect);
        aeDarkSoul!.TimeLeft.Should().Be(2);
    }
    
    [Fact]
    public void MirrorImageIncreasesDurationOfTimeWarp()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useMirrorImage = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [1, 0, 0, 0, 0, 0], [0, 0, 0, 1]);
        var useTimeWarp = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 3,
            [1, 1, 1, 0, 0, 0], [0, 0, 0, 2]);

        // Act
        battleLogic.AbilitiesUsed(1, useMirrorImage, useMirrorImage.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 2);
        battleLogic.AbilitiesUsed(1, useTimeWarp, useTimeWarp.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var aeTimeWarp = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.TimeWarpActiveEffect);
        aeTimeWarp!.TimeLeft.Should().Be(3);
    }
}
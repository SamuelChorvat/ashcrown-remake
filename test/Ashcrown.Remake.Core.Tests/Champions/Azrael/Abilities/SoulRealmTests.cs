using Ashcrown.Remake.Core.Champions.Azrael.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Azrael.Abilities;

public class SoulRealmTests
{
    [Fact]
    public void SoulRealmAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useSoulRealm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1, [1, 0, 0, 0, 0, 0], [1, 0, 0, 0]);

        // Act
        battleLogic.AbilitiesUsed(1, useSoulRealm, useSoulRealm.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        var reapMeAe = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.ReapMeActiveEffect);
        reapMeAe.Should().NotBeNull();

        var soulRealmAe = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.SoulRealmActiveEffect);
        soulRealmAe.Should().NotBeNull();
        soulRealmAe!.Infinite.Should().BeTrue();
        soulRealmAe.Stacks.Should().Be(1);
        soulRealmAe.AllDamageReceiveModifier.Percentage.Should().Be(-25);
    }
    
    [Fact]
    public void SoulRealmStacksCorrectly()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useSoulRealm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1, 0, 0, 0, 0, 0], [1, 0, 0, 0]);

        // Act
        for (var i = 0; i < 3; i++)
        {
            battleLogic.AbilitiesUsed(1, useSoulRealm, useSoulRealm.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);
        }

        // Assert
        var soulRealmAe = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(AzraelConstants.SoulRealmActiveEffect);
        soulRealmAe.Should().NotBeNull();
        soulRealmAe!.Infinite.Should().BeTrue();
        soulRealmAe.Stacks.Should().Be(3);
        soulRealmAe.AllDamageReceiveModifier.Percentage.Should().Be(-75);
    }
    
    [Fact]
    public void SoulRealmCanOnlyBeUsedThreeTimes()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(AzraelConstants.Name);
        var useSoulRealm = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [1, 0, 0, 0, 0, 0], [1, 0, 0, 0]);

        // Act
        for (var i = 0; i < 3; i++)
        {
            battleLogic.AbilitiesUsed(1, useSoulRealm, useSoulRealm.SpentEnergy!).Should().BeTrue();
            BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 6);
        }

        var act = () => battleLogic.AbilitiesUsed(1, useSoulRealm, useSoulRealm.SpentEnergy!);
        

        // Assert
        act.Should().Throw<Exception>().WithMessage("Ability not ready(True) or active(False)");
    }
}
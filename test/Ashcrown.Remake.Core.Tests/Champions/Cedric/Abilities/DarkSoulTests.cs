using Ashcrown.Remake.Core.Champions.Cedric.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Cedric.Abilities;

public class DarkSoulTests
{
    [Fact]
    public void DarkSoulDealsCorrectDamageAndAppliesCorrectActiveEffects()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useDarkSoul = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);

        // Act
        battleLogic.AbilitiesUsed(1, useDarkSoul, useDarkSoul.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        var aeTarget = battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.DarkSoulTargetActiveEffect);
        aeTarget.Should().NotBeNull();
        aeTarget!.TimeLeft.Should().Be(1);

        var aeSelf = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.DarkSoulMeActiveEffect);
        aeSelf.Should().NotBeNull();
    }
    
    [Fact]
    public void DarkSoulBehavesCorrectlyIfTargetIsKilled()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(CedricConstants.Name);
        var useDarkSoul = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 1,
            [0, 0, 0, 1, 0, 0], [0, 0, 0, 1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;
        battleLogic.GetBattlePlayer(2).Champions[0].Health = 10;

        // Act
        battleLogic.AbilitiesUsed(1, useDarkSoul, useDarkSoul.SpentEnergy!).Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);

        // Assert
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(0);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(40);
        battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(CedricConstants.DarkSoulMeActiveEffect).Should().BeFalse();
    
        var aeKill = battleLogic.GetBattlePlayer(1).Champions[0].ActiveEffectController
            .GetActiveEffectByName(CedricConstants.DarkSoulKillActiveEffect);
        aeKill.Should().NotBeNull();
        aeKill!.Infinite.Should().BeTrue();
        aeKill.AllDamageReceiveModifier.Points.Should().Be(-5);
        aeKill.AllDamageDealModifier.Points.Should().Be(5);
    }
}
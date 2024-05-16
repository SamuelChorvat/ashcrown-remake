using Ashcrown.Remake.Core.Champions.Hannibal.Champion;
using Ashcrown.Remake.Core.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Ashcrown.Remake.Core.Tests.Champions.Hannibal.Abilities;

public class HealthFunnelTests
{
    [Fact]
    public void HealthFunnelAppliesCorrectAesDealsDamageAndHeals()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 10;

        // Act
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(10);
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(85);
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .ActiveEffectPresentByActiveEffectName(HannibalConstants.HealthFunnelTargetActiveEffect).Should().BeTrue();
        battleLogic.GetBattlePlayer(2).Champions[0].ActiveEffectController
            .GetActiveEffectByName(HannibalConstants.HealthFunnelTargetActiveEffect)!.TimeLeft.Should().Be(2);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(25);
    }

    [Fact]
    public void HealthFunnelDealsCorrectDamageOverItsDuration()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 5;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 5);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(55);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(50);
    }

    [Fact]
    public void HealthFunnelHealsOnlyHealthLost()
    {
        // Arrange
        var battleLogic = BattleTestSetup.StandardMockedSetupWithSingleChampion(HannibalConstants.Name);
        var usedAbilities = BattleTestHelpers.CreateEndTurnWithOneAbilityUsed(1, 2,
            [0,0,0,1,0,0], [1,0,0,1]);
        battleLogic.GetBattlePlayer(1).Champions[0].Health = 5;
        battleLogic.GetBattlePlayer(2).Champions[0].Health = 10;

        // Act
        var result = battleLogic.AbilitiesUsed(1, usedAbilities, usedAbilities.SpentEnergy!);

        // Assert
        result.Should().BeTrue();
        BattleTestHelpers.PassNumberOfTurns(1, battleLogic, 1);
        battleLogic.GetBattlePlayer(2).Champions[0].Health.Should().Be(0);
        battleLogic.GetBattlePlayer(1).Champions[0].Health.Should().Be(15);
    }
}